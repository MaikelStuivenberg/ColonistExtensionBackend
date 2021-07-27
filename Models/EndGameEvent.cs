using System;
using colonist_extension.Models.EndGame;

namespace colonist_extension.Models
{
    public class EndGameEvent : BaseEvent
    {
        public int Id { get; set; }
        public EndGameEventData Data { get; set; }
    }
}