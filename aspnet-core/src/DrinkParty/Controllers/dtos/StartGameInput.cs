using System;

namespace DrinkParty.Controllers.dtos
{
    public class StartGameInput
    {
        public string PlayerName { get; set; }
        public Guid? RoomCode { get; set; }
    }
}
