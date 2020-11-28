using DrinkParty.Entities;
using DrinkParty.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkParty.Repositories
{
    public class RoomRepository
    {
        //private readonly DrinkDbContext _context;
        //private readonly DbSet<Room> _dbSet;

        //public RoomRepository(DrinkDbContext context)
        //{
        //    _context = context;
        //    _dbSet = context.Rooms;
        //}

        //public async Task<Room> Create(string code)
        //{
        //    var room = new Room
        //    {
        //        Code = code,
        //        MaxUsersLimit = 10,
        //        StartDate = DateTime.Now,
        //    };

        //    await _dbSet.AddAsync(room);
        //    await _context.SaveChangesAsync();

        //    return room;
        //}

        //public async Task<Room> GetByCode(string code)
        //{
        //    return await _dbSet.Include(x => x.Players).FirstOrDefaultAsync(r => r.Code == code);
        //}

    }
}
