using System;
using System.Collections.Generic;

namespace colonist_extension.Models.Database
{
    public class DbEvent
    {
        public int Id { get; set; }
        public string JSON { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}