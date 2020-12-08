using DrinkParty.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DrinkParty.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private readonly GameService _gameService;

        public GameHub(GameService gameService)
        {
            _gameService = gameService;
        }

        private async Task NotifyPlayers()
        {
            var roomId = _gameService.GetCurrentRoomId().ToString();
            var roomPlayers = await _gameService.GetRoomPlayers();
            await Clients.Group(roomId).SendAsync("Players", roomPlayers);
        }

        public override async Task OnConnectedAsync()
        {
            await _gameService.CreatePlayerSessionAsync(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, _gameService.GetCurrentRoomId().ToString());
            await NotifyPlayers();

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await _gameService.RemovePlayerSessionAsync(Context.ConnectionId);
            await NotifyPlayers();

            await base.OnDisconnectedAsync(exception);
        }
    }
}
