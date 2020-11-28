using DrinkParty.Entities;
using DrinkParty.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DrinkParty.Repositories
{
    public class PlayerRepository
    {
        //    private readonly DrinkDbContext _context;
        //    private readonly DbSet<Player> _dbSet;

        //    public PlayerRepository(DrinkDbContext context)
        //    {
        //        _context = context;
        //        _dbSet = context.Players;
        //    }

        //    public async Task<Player> Create(string name, int roomId, bool isAdmin)
        //    {
        //        var user = new Player
        //        {
        //            Name = name,
        //            RoomId = roomId,
        //            IsAdmin = isAdmin,
        //            IsActive = false,
        //        };

        //        await _dbSet.AddAsync(user);
        //        await _context.SaveChangesAsync();

        //        return user;
        //    }

        //    public async Task<Player> Update(Player player)
        //    {
        //        var existingPlayer = await _dbSet.Where(p => p.Id == player.Id).FirstOrDefaultAsync();
        //        existingPlayer = player;
        //        _dbSet.Update(existingPlayer);
        //        await _context.SaveChangesAsync();

        //        return existingPlayer;
        //    }

        //    public async Task<Player> GetByNameAndRoomCode(string playerName, string roomCode)
        //    {
        //        return await _dbSet.Include(p => p.Room).Where(p => p.Name == playerName && p.Room.Code == roomCode).FirstOrDefaultAsync();
        //    }

        //    public async Task<Player> GetByConnectionId(string connectionId)
        //    {
        //        return await _dbSet.Include(p => p.Room).Where(p => p.ConnectionId == connectionId).FirstOrDefaultAsync();
        //    }

        //    public async Task Remove(Player player)
        //    {
        //        _dbSet.Remove(player);
        //        await _context.SaveChangesAsync();

        //    }
        //}
    }
}
