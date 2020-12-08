using DrinkParty.Features.Rooms;

namespace DrinkParty.Features.Game.Dtos
{
    public class GameInfoOutput
    {
        public GameModeType GameMode { get; set; }
        public bool Started { get; set; }
    }
}
