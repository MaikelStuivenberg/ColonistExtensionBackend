using System;
using System.Collections.Generic;

namespace colonist_extension.Models
{
    public class Game
    {
        public int Id { get; set; }
        public IEnumerable<GamePlayer> Players { get; set; }
        public string JSON { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}