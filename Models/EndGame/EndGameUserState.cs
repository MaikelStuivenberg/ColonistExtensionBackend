using System;
using System.Collections.Generic;

namespace colonist_extension.Models.EndGame
{
    public class EndGameUserState
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool PassedTutorial { get; set; }
        public int Type { get; set; }
        public bool AdsEnabled { get; set; }
        public int Icon { get; set; }
        public string KarmaTextStatus { get; set; }
        public string KarmaHoverStatus { get; set; }
        public int CompletedGameCount { get; set; }
    }
}