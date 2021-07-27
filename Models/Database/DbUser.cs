using System;
using System.Collections.Generic;

namespace colonist_extension.Models.Database
{
    public class DbUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}