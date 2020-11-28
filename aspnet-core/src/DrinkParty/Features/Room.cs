using System.Collections.Generic;

namespace DrinkParty.Features
{
    public class Room
    {
        public string Code { get; set; }
        public bool GameStarted { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
