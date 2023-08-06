using Microsoft.EntityFrameworkCore;
using RoomMate.Domain.Domain;
using RoomMate.Persistence.Repositories;
using Shouldly;

namespace RoomMate.Persistence.Test
{
    public class BookingRepositoryTest
    {
        [Fact]
        public void GetAvailableRooms_WhenCalled_Should_Save_Booking()
        {
            // Arrange
            var date = new DateTime(2023, 8, 6);

            var dbOptions = new DbContextOptionsBuilder<RoomMateDbContext>()
                .UseInMemoryDatabase("AvailableRoomTest", x => x.EnableNullChecks(false))
                .Options;

            var booking = new Booking { RoomID = 1, Date = new DateTime(2023 - 06 - 05) };

            using var context = new RoomMateDbContext(dbOptions);
            var bookingRepository = new BookingRepository(context);

            // Act
            bookingRepository.Save(booking);

            // Assert
            var bookings = context.Bookings.ToList();
            bookings.ShouldContain(booking);
            bookings.Where(b => b.Id == booking.Id).Count().ShouldBe(1);
        }

        [Fact]
        public void Save_WhenCalled_Should_Return_Available_Rooms()
        {
            // Arrange
            var date = new DateTime(2023, 8, 6);

            var dbOptions = new DbContextOptionsBuilder<RoomMateDbContext>()
                .UseInMemoryDatabase("AvailableRoomTest", x => x.EnableNullChecks(false))
                .Options;

            using var context = new RoomMateDbContext(dbOptions);
            context.Add(new Room { Id = 1, Name = "Room 1" });
            context.Add(new Room { Id = 2, Name = "Room 2" });
            context.Add(new Room { Id = 3, Name = "Room 3" });

            context.Add(new Booking { RoomID = 1, Date = date });
            context.Add(new Booking { RoomID = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var bookingRepository = new BookingRepository(context);

            // Act
            var availableRooms = bookingRepository.GetAvailableRooms(date);

            // Assert
            availableRooms.Count().ShouldBe(2);
            availableRooms.ShouldContain(x => x.Id == 2);
            availableRooms.ShouldContain(x => x.Id == 3);
            availableRooms.ShouldNotContain(x => x.Id == 1);
        }
    }
}
