using System.Collections.Generic;

namespace Gambit;

public class Game
{
    public string White { get; set; } = "";

    public string Black { get; set; } = "";

    public string WhiteElo { get; set; } = "";

    public string BlackElo { get; set; } = "";

    public string WonBy { get; set; } = "";

    public string Date { get; set; } = "";

    public string TimeControl { get; set; } = "";

    public List<string> Moves { get; set; } = new List<string>();

}