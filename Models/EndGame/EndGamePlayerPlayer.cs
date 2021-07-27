using System;
using System.Collections.Generic;

namespace colonist_extension.Models.EndGame
{
    public class EndGamePlayerPlayer
    {
        public string Id { get; set; }
        public EndGameUserState UserState { get; set; }
        public int Location { get; set; }
        public int Type { get; set; }
        public object Items { get; set; }
        public bool IsReadyToPlay { get; set; }
        public int SelectedColor { get; set; }
    }
}