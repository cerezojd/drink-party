using DrinkParty.Features;
using DrinkParty.Features.Players;
using DrinkParty.Features.Rooms;
using DrinkParty.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DrinkParty.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private readonly PlayerService _playerService;
        private readonly RoomService _roomService;
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        private async Task NotifyRoomGameInfo(string groupName)
        {
            await Clients.Group(groupName).SendAsync("GameInfo");
        }

        public override async Task OnConnectedAsync()
        {
            var roomCode = Guid.Parse(Context.User.FindFirst(ClaimNames.RoomCodeClaimName).Value);
            var playerId = Guid.Parse(Context.UserIdentifier);

            await _gameService.CreatePlayerSessionAsync(roomCode, playerId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _gameService.RemovePlayerSessionAsync(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
