using System;

namespace DrinkParty.Features.Players
{
    public class PlayerSession : Entity<Guid>
    {
        public string ConnectionId { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
