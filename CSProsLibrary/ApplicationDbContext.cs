using CSProsLibrary.Models;
using CSProsLibrary;
using CSProsLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CSProsLibrary;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Country> Countries { get; init; }
    public DbSet<Player> Players { get; init; }
    public DbSet<Game> Games { get; init; }
    public DbSet<Map> Maps { get; init; }
    public DbSet<PlayerSocials> PlayerSocials { get; init; }
    public DbSet<Skin> Skins { get; init; }
    public DbSet<SkinUsage> SkinUsages { get; init; }
    public DbSet<Team> Teams { get; init; }
    public DbSet<Weapon> Weapons { get; init; }
    
    public DbSet<WeaponItem> WeaponItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skin>()
            .Property(s => s.Rarity)
            .HasConversion<string>();

        modelBuilder.Entity<Skin>()
            .HasIndex(s => new { s.WeaponId, s.Name })
            .IsUnique();
        

        modelBuilder.Entity<Team>()
            .HasMany(p => p.Players)
            .WithOne(t => t.Team)
            .HasForeignKey(p => p.TeamId);

        modelBuilder.Entity<Weapon>()
            .Property(w => w.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Weapon>()
            .Property(w => w.TeamAssigned)
            .HasConversion<string>();
        
        modelBuilder.Entity<SkinUsage>()
            .HasKey(p => new { p.PlayerId, p.GameId, p.SkinId });
        
        modelBuilder.Entity<SkinUsage>()
            .HasOne<Player>() // Assuming navigation properties are defined
            .WithMany()
            .HasForeignKey(su => su.PlayerId)
            .OnDelete(DeleteBehavior.Restrict); // or Cascade based on your model requirements

        modelBuilder.Entity<SkinUsage>()
            .HasOne<Game>()
            .WithMany()
            .HasForeignKey(su => su.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        // If Skin is a separate entity
        modelBuilder.Entity<SkinUsage>()
            .HasOne<Skin>() // Assuming Skin is an entity with navigation properties defined
            .WithMany()
            .HasForeignKey(su => su.SkinId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Player>()
            .HasIndex(p => p.GamerTag)
            .IsUnique();
        
        modelBuilder.Entity<Player>()
            .HasOne(p => p.Country)
            .WithMany()
            .HasForeignKey(p => p.CountryId);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId);
        
        modelBuilder.Entity<Game>()
            .HasMany(g => g.SkinUsages)  // Assuming you add this navigation property
            .WithOne() // No need to specify if no reverse navigation
            .HasForeignKey(su => su.GameId);
        
        modelBuilder.Entity<PlayerSocials>()
            .HasKey(p => p.PlayerId);
            
        base.OnModelCreating(modelBuilder);
    }

    // Configure models and relationships in the OnModelCreating method if needed
}