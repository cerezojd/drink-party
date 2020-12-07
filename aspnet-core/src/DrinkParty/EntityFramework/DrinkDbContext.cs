using DrinkParty.Features.Players;
using DrinkParty.Features.Rooms;
using Microsoft.EntityFrameworkCore;

namespace DrinkParty.EntityFramework
{
    public class DrinkDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSession> PlayerSessions { get; set; }

        public DrinkDbContext(DbContextOptions<DrinkDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Room>(c =>
            {
                c.HasKey(r => r.Id);
                c.HasIndex(r => r.Code);
                c.HasMany(r => r.Players).WithOne(r => r.Room).HasForeignKey(x => x.RoomId);
            });

            mb.Entity<Player>(c =>
            {
                c.HasKey(c => c.Id);
                c.HasOne(p => p.Room).WithMany(r => r.Players).HasForeignKey(p => p.RoomId);
                c.HasMany(p => p.Sessions).WithOne(s => s.Player).HasForeignKey(s => s.PlayerId);
            });

            mb.Entity<PlayerSession>(c => {
                c.HasKey(s => s.Id);
                c.HasIndex(s => s.ConnectionId);
                c.HasOne(s => s.Player).WithMany(p => p.Sessions).HasForeignKey(s => s.PlayerId);
            });

            base.OnModelCreating(mb);
        }
    }
}
