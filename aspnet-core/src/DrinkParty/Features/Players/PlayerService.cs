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

        public async Task<Player> GetByIdAsync(Guid playerId, bool includeSessions, bool tracking = true)
        {
            var query = _playerDbSet.AsQueryable();
            if (includeSessions)
                query = query.Include(p => p.Sessions);
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task AddSessionAsync(Guid playerId, string connectionId)
        {
            var player = await GetByIdAsync(playerId, true, false);
            if (player is null)
                throw new Exception("Player does not exist");

            if (player.Sessions.Any(s => s.ConnectionId == connectionId))
                throw new Exception("Session already added");

            var session = new PlayerSession
            {
                ConnectionId = connectionId,
                PlayerId = playerId
            };

            await _playerSessionDbSet.AddAsync(session);
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
            var player = await GetByIdAsync(playerId, true, false);
            if (player is null)
                throw new Exception("Player does not exist");

            return player.Sessions;
        }

        public async Task RemoveAsync(Guid id)
        {
            var player = await GetByIdAsync(id, false, false);
            if (player is null)
                throw new Exception("Player does not exist");

            _playerDbSet.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task AssingPlayerAdminAsync(Guid roomId)
        {
            var players = await _playerDbSet.Include(r => r.Sessions).Where(p => p.RoomId == roomId).ToArrayAsync();

            var lastAdmin = players.FirstOrDefault(p => p.IsAdmin);
            var newAdmin = players.Where(p => p.Sessions.Any()).FirstOrDefault();

            if (!(lastAdmin is null) && !(newAdmin is null))
            {
                lastAdmin.IsAdmin = false;
                newAdmin.IsAdmin = true;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Player>> GetWithActiveSessionsByRoomIdAsync(Guid roomId)
        {
            return await _playerDbSet.Where(p => p.RoomId == roomId && p.Sessions.Any()).ToArrayAsync();
        }

        public async Task<Player> GetAdminByRoomId(Guid roomId)
        {
            return await _playerDbSet.Include(p => p.Sessions).FirstOrDefaultAsync(p => p.RoomId == roomId && p.IsAdmin);
        }
    }
}
