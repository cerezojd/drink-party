using DrinkParty.Features.Rooms;
using System;
using System.Collections.Generic;

namespace DrinkParty.Features.Players
{
    public class Player : Entity<Guid>
    {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        
        public Room Room { get; set; }
        public Guid RoomId { get; set; }

        public ICollection<PlayerSession> Sessions { get; set; } = new List<PlayerSession>();
    }
}
