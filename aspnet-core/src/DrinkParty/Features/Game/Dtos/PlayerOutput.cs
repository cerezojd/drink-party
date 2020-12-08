using System;

namespace DrinkParty.Features.Game.Dtos
{
    public class PlayerOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }
}
