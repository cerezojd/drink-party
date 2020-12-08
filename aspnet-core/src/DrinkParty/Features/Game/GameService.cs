using DrinkParty.EntityFramework;
using DrinkParty.Features.Game.Dtos;
using DrinkParty.Features.Players;
using DrinkParty.Features.Rooms;
using DrinkParty.Jwt;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace DrinkParty.Features
{
    public class GameService
    {
        private readonly RoomService _roomService;
        private readonly PlayerService _playerService;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly TransactionService _transactionService;
        private readonly IHttpContextAccessor _accessor;

        public GameService(RoomService roomService, PlayerService playerService, JwtTokenGenerator jwtTokenGenerator, TransactionService transactionService, IHttpContextAccessor accessor)
        {
            _roomService = roomService;
            _playerService = playerService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _transactionService = transactionService;
            _accessor = accessor;
        }

        public async Task<string> CreateGameAsync(string playerName)
        {
            var player = await _playerService.CreateAsync(playerName);
            var room = await _roomService.CreateAsync();

            await _roomService.JoinAsyn(room.Id, player);
            return _jwtTokenGenerator.GenerateJwtToken(room.Id.ToString(), player.Id.ToString(), player.Name);
        }

        public async Task<string> JoinGameAsync(Guid roomId, string playerName)
        {
            string token;
            try
            {
                await _transactionService.StartAsync();

                var player = await _playerService.CreateAsync(playerName);
                var room = await _roomService.JoinAsyn(roomId, player);
                token = _jwtTokenGenerator.GenerateJwtToken(room.Id.ToString(), player.Id.ToString(), player.Name);
            }
            catch
            {
                await _transactionService.RollbackAsync();
                throw;
            }

            await _transactionService.CommitAsync();
            return token;
        }

        public async Task CreatePlayerSessionAsync(string connectionId)
        {
            var room = await _roomService.GetByIdAsync(GetCurrentRoomId(), true);
            if (room is null)
                throw new Exception("Invalid room");

            var player = room.Players.FirstOrDefault(p => p.Id == GetCurrentPlayerId());
            if (player is null)
                throw new Exception("Player does not exist");

            await _playerService.AddSessionAsync(player.Id, connectionId);

            // Check is player admin has sessions
            var currentAdmin = await _playerService.GetAdminByRoomId(room.Id);
            if (!currentAdmin.Sessions.Any())
                await _playerService.AssingPlayerAdminAsync(room.Id);
        }

        public async Task RemovePlayerSessionAsync(string connectionId)
        {
            var playerId = GetCurrentPlayerId();
            var roomId = GetCurrentRoomId();

            var player = await _playerService.GetByIdAsync(playerId, true, true);
            if (!player.Sessions.Any(s => s.ConnectionId == connectionId))
                throw new Exception("Session does not exist");

            await _playerService.RemoveSessionAsync(connectionId);

            if (player.IsAdmin && !player.Sessions.Any())
                await _playerService.AssingPlayerAdminAsync(roomId);
        }

        public async Task<IEnumerable<PlayerOutput>> GetRoomPlayers()
        {
            var players = await _playerService.GetWithActiveSessionsByRoomIdAsync(GetCurrentRoomId());
            var result = players.Select(p => new PlayerOutput
            {
                Id = p.Id,
                Name = p.Name,
                IsAdmin = p.IsAdmin
            });

            return result;
        }

        public Guid GetCurrentRoomId() => Guid.Parse(_accessor.HttpContext.User.FindFirst(ClaimNames.RoomCodeClaimName).Value);

        public Guid GetCurrentPlayerId() => Guid.Parse(_accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
