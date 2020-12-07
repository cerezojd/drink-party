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

        public async Task<Guid> CreateAsync()
        {
            var room = new Room
            {
                Code = Guid.NewGuid(),
                GameStarted = false,
            };

            await _dbSet.AddAsync(room);
            await _context.SaveChangesAsync();

            return room.Id;
        }

        public async Task<Guid> JoinAsyn(Guid roomCode, Player player)
        {
            var room = await _dbSet.Include(r => r.Players).FirstOrDefaultAsync(r => r.Code == roomCode);
            if (room is null)
                throw new Exception("Room does not exist");

            if (room.Players.Any(p => p.Id == player.Id))
                throw new Exception("Player already joined");

            if(room.Players.Count == room.MaxPlayer)
                throw new Exception("Room full");

            if (room.Players.Count == 0)
                player.IsAdmin = true;

            room.Players.Add(player);

            await _context.SaveChangesAsync();

            return room.Id;
        }
    }
}
