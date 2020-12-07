using DrinkParty.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkParty.Features.Players
{
    public class PlayerService
    {
        private readonly DrinkDbContext _context;
        private readonly DbSet<Player> _playerDbSet;
        private readonly DbSet<PlayerSession> _playerSessionDbSet;

        public PlayerService(DrinkDbContext context)
        {
            _context = context;
            _playerDbSet = context.Players;
            _playerSessionDbSet = context.PlayerSessions;
        }


        public async Task<Player> CreateAsync(string name)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                IsAdmin = false,
                Name = name,
            };

            await _playerDbSet.AddAsync(player);
            await _context.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetByIdAsync(Guid playerId)
        {
            return await _playerDbSet.Include(p => p.Sessions).FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task AddSessionAsync(Guid playerId, string connectionId)
        {
            var player = await GetByIdAsync(playerId);
            if (player is null)
                throw new Exception("Player does not exist");

            if (player.Sessions.Any(s => s.ConnectionId == connectionId))
                throw new Exception("Session already added");

            var session = new PlayerSession
            {
                ConnectionId = connectionId,
                PlayerId = playerId
            };

            player.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSessionAsync(string connectId)
        {
            var session = await _playerSessionDbSet.FirstOrDefaultAsync(s => s.ConnectionId == connectId);
            if (session is null)
                throw new Exception("Session doest not exist");

            _playerSessionDbSet.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlayerSession>> GetSessionsAsync(Guid playerId)
        {
            var player = await GetByIdAsync(playerId);
            if (player is null)
                throw new Exception("Player does not exist");

            return player.Sessions;
        }

        public async Task<PlayerSession> GetSessionByConnectionIdAsync(string connectioId)
        {
            return await _playerSessionDbSet.Include(p => p.Player).ThenInclude(p => p.Room).FirstOrDefaultAsync(s => s.ConnectionId == connectioId);
        }

        public async Task RemoveAsync(Guid id)
        {
            var player = await GetByIdAsync(id);
            if (player is null)
                throw new Exception("Player does not exist");

            _playerDbSet.Remove(player);
            await _context.SaveChangesAsync();
        }
    }
}
