using Avalonia;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            // FOR EACH game in the list:
            foreach (JsonElement gameData in games.EnumerateArray())
            {
                // GET the PGN string
                string pgn = gameData.GetProperty("pgn").GetString() ?? "";

                // PARSE the PGN into a game object
                Game game = parser.ParseGame(pgn);

                // PRINT game.White and game.Moves.Count
                Console.WriteLine(game.White);
                Console.WriteLine(game.Moves.Count + " moves");
            }

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
