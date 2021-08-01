using System;
using System.Collections.Generic;

namespace colonist_extension.Models
{
    public class GamePlayer
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool Winner { get; set; }
    }
}