using DrinkParty.Features;
using System;

namespace DrinkParty.Features
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
