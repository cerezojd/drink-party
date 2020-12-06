using DrinkParty.Controllers.dtos;
using DrinkParty.Jwt;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DrinkParty.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController: ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public GameController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("StartGame")]
        public StartGameOutput StartGame([FromBody] StartGameInput input)
        {

            var roomCode = input.RoomCode ?? Guid.NewGuid().ToString();
            if (!Guid.TryParse(roomCode, out var _))
                throw new Exception("Invalid room code");

            var token = _jwtTokenGenerator.GenerateJwtToken(input.PlayerName, roomCode);

            return new StartGameOutput { Token = token };
        }
    }
}
