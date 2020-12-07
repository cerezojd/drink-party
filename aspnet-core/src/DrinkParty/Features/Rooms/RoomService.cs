using DrinkParty.EntityFramework;
using DrinkParty.Features.Players;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkParty.Features.Rooms
{
    public class RoomService
    {
        private readonly DrinkDbContext _context;
        private readonly DbSet<Room> _dbSet;

        public RoomService(DrinkDbContext context)
        {
            _context = context;
            _dbSet = context.Rooms;
        }

        public async Task<Room> CreateAsync()
        {
            var room = new Room
            {
                GameStarted = false,
            };

            await _dbSet.AddAsync(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<Room> JoinAsyn(Guid roomId, Player player)
        {
            var room = await GetRoomByCodeAsync(roomId);
            if (room is null)
                throw new Exception("Room does not exist");

            if (room.Players.Any(p => p.Id == player.Id))
                throw new Exception("Player already joined");

            if (room.Players.Count == room.MaxPlayer)
                throw new Exception("Room full");

            if (room.Players.Count == 0)
                player.IsAdmin = true;

            room.Players.Add(player);

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task AssingPlayerAdminAsync(Guid roomId)
        {
            var room = await _dbSet.Include(r => r.Players).ThenInclude(p => p.Sessions).FirstOrDefaultAsync(r => r.Id == roomId);

            var lastAdmin = room.Players.FirstOrDefault(p => p.IsAdmin);
            var newAdmin = room.Players.Where(p => p.Sessions.Any()).FirstOrDefault();

            if (!(lastAdmin is null) && !(newAdmin is null))
            {
                lastAdmin.IsAdmin = false;
                newAdmin.IsAdmin = true;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Room> GetRoomByCodeAsync(Guid roomId)
        {
            return await _dbSet.Include(r => r.Players).FirstOrDefaultAsync(r => r.Id == roomId);
        }
    }
}
