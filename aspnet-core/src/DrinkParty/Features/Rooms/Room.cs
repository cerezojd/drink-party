using DrinkParty.Features.Players;
using System;
using System.Collections.Generic;

namespace DrinkParty.Features.Rooms
{
    public class Room : Entity<Guid>
    {
        public bool GameStarted { get; set; }
        public int MaxPlayer { get; set; } = 10;
        public GameModeType GameMode { get; set; } = GameModeType.None;

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
