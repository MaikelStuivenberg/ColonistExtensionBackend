using System;
using System.Collections.Generic;

namespace colonist_extension.Models.EndGame
{
    public class EndGamePlayer
    {
        public bool WinningPlayer { get; set; }
        public EndGamePlayerPlayer Player { get; set; }
        public object VictoryPoints { get; set; }
        public int Rank { get; set; }
        public string RankText { get; set; }
    }
}