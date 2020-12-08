using DrinkParty.Features;
using DrinkParty.Features.Rooms;
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
            var roomId = _gameService.CurrentRoomId.ToString();
            var roomPlayers = await _gameService.GetRoomPlayers();
            await Clients.Group(roomId).SendAsync("Players", roomPlayers);
        }

        private async Task NotifyGameInfoToAll()
        {
            var roomId = _gameService.CurrentRoomId.ToString();
            var gameInfo = await _gameService.GetGameInfo();
            await Clients.Group(roomId).SendAsync("GameInfo", gameInfo);
        }

        private async Task NotifyGameInfoToPlayer()
        {
            var gameInfo = await _gameService.GetGameInfo();
            await Clients.Caller.SendAsync("GameInfo", gameInfo);
        }

        public async Task ChooseGameMode(GameModeType mode)
        {
            await _gameService.SetGameMode(mode);
            await NotifyGameInfoToAll();
        }

        public override async Task OnConnectedAsync()
        {
            await _gameService.CreatePlayerSessionAsync(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, _gameService.CurrentRoomId.ToString());
            await NotifyPlayers();
            await NotifyGameInfoToPlayer();

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
