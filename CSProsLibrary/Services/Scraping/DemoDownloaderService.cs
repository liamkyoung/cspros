using CSProsLibrary.Models;
using CSProsLibrary.Models.Dtos;
using CSProsLibrary.Services.Interfaces;
using CSProsLibrary.Services.Interfaces.Scraping;
using CSProsLibrary.Services.Scraping.Pages;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace CSProsLibrary.Services.Scraping;

public class DemoDownloaderService : IDemoDownloaderService
{
    private readonly IGameService _gameService;
    private readonly ITeamService _teamService;
    private readonly IPlayerService _playerService;
    private readonly MatchPage _matchPage;
    private readonly ResultsListPage _resultsPage;
    private readonly ILogger<DemoDownloaderService> _logger;
    private readonly IWebDriver _driver;
    
    private static readonly string DEMO_EXTRACT_DIR = Environment.GetEnvironmentVariable("DEMO_ANALYZE_DIR")!;
    private static readonly string DEMO_DOWNLOAD_DIR = Environment.GetEnvironmentVariable("DEMO_DOWNLOAD_DIR")!;
    private bool TeamShouldBeUpdated(Team team) =>
        (team.TimeSinceLastUpdated + TimeSpan.FromDays(7)) < DateTimeOffset.Now;

    public DemoDownloaderService(IWebDriver driver, MatchPage matchPage, ResultsListPage resultsPage, IGameService gameService, ITeamService teamService, IPlayerService playerService, ILogger<DemoDownloaderService> logger)
    {
        _driver = driver;
        _matchPage = matchPage;
        _resultsPage = resultsPage;
        _logger = logger;
        _gameService = gameService;
        _teamService = teamService;
        _playerService = playerService;
    }
    
    // Download Algorithm:
    // 1. Find all matches
    // 2. For each match, check if match already exists.
        // 3. If not, then get match details
        // 3a. Get Teams Participating
        // 3b. Update team if necessary (it's been 7 days) or add to DB if not existent
        // 3c. Try to add/update all players from team into DB
        

    public async Task Start()
    {
        var matchLinks = _resultsPage.GetMatches();

        foreach (var matchUrl in matchLinks)
        {
            if (await _gameService.HasGameBeenProcessed(matchUrl))
            {
                _logger.LogInformation($"Game {matchUrl} has already been processed, moving to next game.");
                continue;
            }
            
            var parsedMatchPage = await GetParsedMatchResultData(matchUrl);

            if (parsedMatchPage == null)
            {
                continue;
            }

            // Get teams, their players, and add to DB
            var teamA = await ProcessTeamAndPlayerData(parsedMatchPage.First().TeamAName, parsedMatchPage.First().TeamAProfileLink);
            var teamB = await ProcessTeamAndPlayerData(parsedMatchPage.First().TeamBName, parsedMatchPage.First().TeamBProfileLink);

            if (teamA == null || teamB == null)
            {
                continue;
            }
            
            // Generate game entry based on parsed match

            foreach (var matchInfo in parsedMatchPage)
            {
                var game = await _gameService.GenerateGameFromMatchData(matchInfo, teamA, teamB);
                
                if (game == null)
                {
                    _logger.LogError($"Could not generate game from match data: {matchInfo.HltvLink}");
                    continue;
                }
            
                await _gameService.AddGameAsync(game);
                _matchPage.GoToPage(matchUrl);
            }
            
            _matchPage.DownloadDemosForMatch(); // Download before or after parsing game info?
            // associate game with demo name
        }
        
        _driver.Quit();
    }
    
    private async Task<Team?> ProcessTeamAndPlayerData(string teamName, string teamProfileLink)
    {
        var team = await _teamService.GetTeamByName(teamName);
        
        if (team == null || TeamShouldBeUpdated(team))
        {
            var teamAProfile = _teamService.ParseTeamPage(teamProfileLink);

            if (teamAProfile == null)
            {
                return null;
            }
                
            team = await _teamService.AddTeamAsync(teamAProfile);

            // Add or update each player that's found.
            foreach (var playerHltvLink in teamAProfile.PlayerHltvLinks)
            {
                var player = await _playerService.GetPlayerByHltvLink(playerHltvLink);

                if (player == null)
                {
                    var playerProfile = await _playerService.ParsePlayerProfile(playerHltvLink);

                    if (playerProfile == null)
                    {
                        continue;
                    }
                    
                    await _playerService.AddPlayer(playerProfile);
                }
                else
                {
                    await _playerService.UpdatePlayer(player);
                }
            }
            // await _playerService.AddPlayersByHltvLinks(teamAProfile.PlayerHltvLinks);
            // TODO: Player Socials
        }

        return team;
    }
    

    private async Task<IEnumerable<MatchResultDto>?> GetParsedMatchResultData(string matchUrl)
    {
        _matchPage.PageHref = matchUrl;
        var hltvGameLink = _matchPage.PageHref;

        _matchPage.GoToPage(matchUrl);
        // Get game info
        var parsedGameInfo = _matchPage.GetInfoForMatches();

        if (!parsedGameInfo.Any())
        {
            _logger.LogError($"Could not parse game info for: {matchUrl}");
            return null;
        }

        return parsedGameInfo;
    }

    public void ManuallyExtractDemos()
    {
        if (!Directory.Exists(DEMO_DOWNLOAD_DIR))
        {
            _logger.LogInformation($"Input path does not exist: {DEMO_DOWNLOAD_DIR}");
            return;
        }
        
        _logger.LogInformation($"Extracting demo files...");

        try
        {
            foreach (var fileName in Directory.GetFiles(DEMO_DOWNLOAD_DIR, "*.rar"))
            {
                if (!fileName.EndsWith(".rar"))
                {
                    continue;
                }
                
                using (var archive = ArchiveFactory.Open(fileName))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(DEMO_EXTRACT_DIR, GenerateExtractionOptions());
                        }
                    }
                }

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    _logger.LogInformation($"[Deleted] Archive {fileName}");
                }
            }
            
            _logger.LogInformation("Finished extracting...");
        }
        catch (Exception e)
        {
            _logger.LogError(e.StackTrace);
        }

        ExtractionOptions GenerateExtractionOptions()
        {
            return new ExtractionOptions()
            {
                Overwrite = true,
                ExtractFullPath = true,
                PreserveFileTime = true,
                PreserveAttributes = true
            };
        }
    }
}