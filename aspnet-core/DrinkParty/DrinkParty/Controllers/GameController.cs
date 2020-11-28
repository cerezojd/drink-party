using DrinkParty.Features.Rooms;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class CreateRoomInput
{
    public string PlayerName { get; set; }
}

public class JoinRoomInput
{
    public string PlayerName { get; set; }
    public string RoomCode { get; set; }
}


namespace DrinkParty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController: ControllerBase
    {
        //private readonly GameService _roomService;

        //public GameController(GameService roomService)
        //{
        //    _roomService = roomService;
        //}

        //[HttpPost("create")]
        //public async Task<string> CreateRoom([FromBody] CreateRoomInput input)
        //{
        //    return await _roomService.CreateRoom(input.PlayerName);
        //}

        //[HttpPost("join")]
        //public async Task JoinRoom([FromBody] JoinRoomInput input)
        //{
        //    await _roomService.JoinRoom(input.RoomCode, input.PlayerName);
        //}
    }
}
