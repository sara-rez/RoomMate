using Microsoft.EntityFrameworkCore;
using RoomMate.Domain.Domain;

namespace RoomMate.Persistence.Repositories
{
    public class RoomMateDbContext : DbContext
    {
        public RoomMateDbContext(DbContextOptions<RoomMateDbContext> options):
            base(options)        
        {
                
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "room1"},
                new Room { Id = 2, Name = "room2" },
                new Room { Id = 3, Name = "room3" }
                );
        }
    }
}
