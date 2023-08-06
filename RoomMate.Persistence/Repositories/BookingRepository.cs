using RoomMate.Core.Interfaces;
using RoomMate.Domain.Domain;

namespace RoomMate.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly RoomMateDbContext _context;

        public BookingRepository(RoomMateDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            return _context.Rooms
                .Where(r => !r.Bookings.Any(x => x.Date == date))
                .ToList();
        }

        public void Save(Booking booking)
        {
            _context.Add(booking);
            _context.SaveChanges();
        }
    }
}
