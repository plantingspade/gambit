using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using Avalonia.Controls;

namespace Gambit;

class PgnParser
{
    /// <summary>
    /// Turn RAW PGN into a "Game" object with metadata (players, date, result)
    /// </summary>
    /// <param name="pgn"></param>
    /// <returns></returns>
    public Dictionary<string, string> ParseHeaders(string pgn)
    {
        // Declare an empty dictionary to collect results as program loops
        Dictionary<string, string> headers = new Dictionary<string, string>();

        // Split string into lines
        string[] lines = pgn.Split('\n');

        // FOR EACH line in the pgn string
        foreach (string line in lines)
        {
            // IF the line starts with "["
            if (line.StartsWith("["))
            {
                // Split the line on "
                string[] parts = line.Split('"');

                // key = first return (with "[" stripped off)
                // value = middle return
                headers[parts[0]] = parts[1];
            }
        }

        // Return key-value pairs
        return headers;

    }

    /// <summary>
    /// Turn RAW PGN into a list of "Move" objects
    /// </summary>
    /// <param name="pgn"></param>
    /// <returns></returns>
    public List<string> ParseMoves(string pgn)
    {
        // Find the part of the string after the blank line
        string[] sections = pgn.Split(new string[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.None);
        string moveText = sections[1];

        // Remove anything that looks like 1., 2., 3. ...
        moveText = Regex.Replace(moveText, @"\d+\.", "");

        // Split the remaining text on spaces
        List<string> moves = new List<string>(moveText.Split(' '));

        // RETURN list of moves
        return moves;
    }

    public Game ParseGame(string pgn)
    {
        // Create a new Game object
        Game game = new Game();

        // Call ParseHeaders to get the headers dictionary
        Dictionary<string, string> headers = ParseHeaders(pgn);
        

        // Parse headers into Game properties
        game.White = headers["[White "];
        game.Black = headers["[Black "];
        game.WhiteElo = headers["[WhiteElo "];
        game.BlackElo = headers["[BlackElo "];
        game.WonBy = headers["[Termination "];
        game.Date = headers["[Date "];
        game.TimeControl = headers["[TimeControl "];

        // Call ParseMoves to get the list of moves
        List<string> moves = ParseMoves(pgn);

        // Parse moves into Game properties
        game.Moves = moves;

        return game;
    }
}