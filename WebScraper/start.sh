#!/bin/bash
echo "[CSPROS] Launching CSPros Web Scraper..."
DEMO_DOWNLOAD_DIR=~/Desktop/Demos_Download
DEMO_ANALYZE_DIR=~/Desktop/Demos_Extracted
cd ~/Desktop/Projects/cs2-demoanalyzer/WebScraper
~/.dotnet/dotnet run &
SCRIPT_PID=$!
sleep 3300  # Sleep for 55 minutes (55 * 60 seconds), Will be run every hour.
kill $SCRIPT_PID
echo "[CSPROS] Finished scraping job."
