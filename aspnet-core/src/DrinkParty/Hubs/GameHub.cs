using DrinkParty.Entities;
using DrinkParty.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace DrinkParty.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private static List<Room> rooms = new List<Room>();

        public GameHub()
        {
        }

        public async Task<string> CreateRoom(string playerName)
        {
            var player = new Player { ConnectionId = Context.ConnectionId, IsAdmin = true, Username = playerName };
            var room = new Room
            {
                Code = Guid.NewGuid().ToString(),
                GameStarted = false,
                Players = new List<Player> { player },
            };

            rooms.Add(room);
            await Groups.AddToGroupAsync(Context.ConnectionId, room.Code);
            return room.Code;
        }

        public async Task JoinRoom(string roomCode, string playerName)
        {
            var room = rooms.FirstOrDefault(r => r.Code == roomCode);
            if (room is null)
                throw new Exception("Room does not exist");

            if(room.Players.Any(p => p.Username == playerName))
                throw new Exception("Already exists a user with that name");

            var player = new Player { ConnectionId = Context.ConnectionId, IsAdmin = false, Username = playerName };
            room.Players.Add(player);

            await Groups.AddToGroupAsync(Context.ConnectionId, room.Code);
        }


        private async Task NotifyRoomGameInfo(string groupName)
        {
            await Clients.Group(groupName).SendAsync("GameInfo", GetGameInfo(groupName));
        }

        private GameInfo GetGameInfo(string roomCode)
        {
            var room = rooms.FirstOrDefault(r => r.Code == roomCode);
            return !(room is null) ? new GameInfo { Players = room.Players.ToArray(), Started = room.GameStarted, RoomCode = room.Code } : null;
        }

        public override Task OnConnectedAsync()
        {
            var context = Context.UserIdentifier;
            return base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
