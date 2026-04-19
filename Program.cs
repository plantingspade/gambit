using Avalonia;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace Gambit;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]

    public static async Task Main(string[] args)
    {
        try
        {
            // Declare variables
            HttpClient client = new HttpClient(); // Makes HTTP requests to the web
            string json = ""; // JSON from Chess.com will go here
            PgnParser parser = new PgnParser(); // Parses raw PGN strings into structured data


            // Call Chess.com API, GET raw JSON file
            client.DefaultRequestHeaders.Add("User-Agent", "Gambit/1.0"); // Chess.com requires this to identify the app making the request
            json = await client.GetStringAsync("https://api.chess.com/pub/player/plantingspade/games/2026/04");

            // PARSE the JSON into a document
            JsonDocument doc = JsonDocument.Parse(json);

            // GET the "games" list from the document
            JsonElement games = doc.RootElement.GetProperty("games");

            // Create an empty list to hold game objects
            List<Game> gameList = new List<Game>();

            // FOR EACH game in the list:
            foreach (JsonElement gameData in games.EnumerateArray())
            {
                // GET the PGN string
                string pgn = gameData.GetProperty("pgn").GetString() ?? "";

                // PARSE the PGN into a game object
                Game game = parser.ParseGame(pgn);

                // Add each game to the list
                gameList.Add(game);

                // PRINT game.White and game.Moves.Count
                Console.WriteLine(game.White);
                Console.WriteLine(game.Moves.Count + " moves");
            }

            // Create a temporary directory to hold game JSON files
            string tempDir = Path.Combine(Path.GetTempPath(), "Gambit"); // Asks the OS for a temp directory and adds "Gambit" to the path
            Directory.CreateDirectory(tempDir); // Creates the directory if it doesn't exist (does nothing if it already exists)
            
            Console.WriteLine("Saving to: " + tempDir); // Print the directory path to the console so we can find the files later

            // FOR EACH game in the list:
            for (int i = 0; i < gameList.Count; i++)
            {
                // Convert the game object to JSON and save it to a file
                string gameJson = JsonSerializer.Serialize(gameList[i]); // Convert the game object to JSON
                string filePath = Path.Combine(tempDir, $"game{i}.json"); // File path
                File.WriteAllText(filePath, gameJson); // Creates the file and writes the JSON to it
            }

            // Delete the temporary directory and all its contents when the program exits
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Directory.Delete(tempDir, true);

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }

        // CATCH & DISPLAY runtime errors
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
