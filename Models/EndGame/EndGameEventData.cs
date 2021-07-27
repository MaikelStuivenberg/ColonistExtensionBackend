using System;
using System.Collections.Generic;

namespace colonist_extension.Models.EndGame
{
    public class EndGameEventData
    {
        public List<EndGamePlayer> Players { get; set; }
        public List<object> DiceStats { get; set; }
        public List<object> ResourceCardStats { get; set; }
        public List<object> DevelopmentCardStats { get; set; }
        public List<object> ActivityStats { get; set; }
        public int Time { get; set; }
        public int DiceCount { get; set; }
        public List<object> ResourceStats { get; set; }
    }
}