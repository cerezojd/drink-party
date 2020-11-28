using DrinkParty.Entities;
using DrinkParty.Features;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace DrinkParty.Hubs
{


    public class GameHub : Hub
    {
        private static List<Room> rooms = new List<Room>();


        public GameHub()
        {
        }


        public async Task CreateRoom(string playerName)
        {
            var player = new Player { ConnectionId = Context.ConnectionId, IsAdmin = true, Name = playerName };
            var room = new Room
            {
                Code = Guid.NewGuid().ToString(),
                GameStarted = false,
                Players = new List<Player> { player },
            };

            rooms.Add(room);
            await Groups.AddToGroupAsync(Context.ConnectionId, room.Code);
            await Clients.Caller.SendAsync("gameInfo", GetGameInfo(room.Code));
        }


        private GameInfo GetGameInfo(string roomCode)
        {
            var room = rooms.FirstOrDefault(r => r.Code == roomCode);
            return !(room is null) ? new GameInfo { Players = room.Players.ToArray(), Started = room.GameStarted, RoomCode = room.Code } : null;
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
