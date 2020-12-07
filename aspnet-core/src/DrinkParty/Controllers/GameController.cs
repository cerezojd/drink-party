using DrinkParty.Controllers.dtos;
using DrinkParty.Features;
using DrinkParty.Features.Rooms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrinkParty.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController: ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("StartGame")]
        public async Task<StartGameOutput> StartGameAsync([FromBody] StartGameInput input)
        {
            if (string.IsNullOrWhiteSpace(input.PlayerName))
                throw new Exception("Invalid player name");

            string token;
            if (input.RoomCode.HasValue)
                token = await _gameService.JoinGameAsync(input.RoomCode.Value, input.PlayerName);
            else
                token = await _gameService.CreateGameAsync(input.PlayerName);

            return new StartGameOutput { Token = token };
        }
    }
}
