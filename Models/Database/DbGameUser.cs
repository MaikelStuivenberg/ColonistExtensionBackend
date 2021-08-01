using System;
using System.Collections.Generic;

namespace colonist_extension.Models.Database
{
    public class DbGameUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool Winner { get; set; }
        public int GameId { get; set; }
    }
}