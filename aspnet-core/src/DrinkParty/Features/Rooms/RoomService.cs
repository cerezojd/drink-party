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
            var room = await GetByIdAsync(roomId, true);
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
 
        public async Task<Room> GetByIdAsync(Guid roomId, bool includePlayers, bool trancking = true)
        {
            var queryable = _dbSet.AsQueryable();
            if (includePlayers)
                queryable = queryable.Include(r => r.Players);
            if (!trancking)
                queryable = queryable.AsNoTracking();

            return await queryable.FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task ChangeGameModeAsync(Guid roomId, GameModeType mode)
        {
            var room = await GetByIdAsync(roomId, true);
            if (room is null)
                throw new Exception("Room does not exist");


            room.GameMode = mode;
            await _context.SaveChangesAsync();
        }
    }
}
