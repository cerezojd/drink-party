using DrinkParty.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkParty.EntityFramework
{
    public class DrinkDbContext : DbContext
    {
        //public DbSet<Room> Rooms { get; set; }
        //public DbSet<Player> Players { get; set; }
        //public DbSet<Question> Questions { get; set; }
        //public DbSet<RoomQuestion> RoomQuestions { get; set; }

        //public DrinkDbContext(DbContextOptions<DrinkDbContext> options) : base(options)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder mb)
        //{
        //    mb.Entity<Player>(c =>
        //    {
        //        c.HasOne(u => u.Room).WithMany(r => r.Players).HasForeignKey(r => r.RoomId);
        //    });

        //    mb.Entity<Room>(c =>
        //    {
        //        c.HasMany(r => r.Players).WithOne(r => r.Room).HasForeignKey(x => x.RoomId);
        //    });

        //    mb.Entity<RoomQuestion>(c =>
        //    {
        //        c.HasKey(x => new { x.QuestionId, x.RoomId });
        //        c.HasOne(u => u.Question).WithOne();
        //        c.HasOne(u => u.Room).WithOne();
        //    });

        //    base.OnModelCreating(mb);
        //}
    }
}
