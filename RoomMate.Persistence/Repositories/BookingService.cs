using RoomMate.Core.Interfaces;
using RoomMate.Domain.Domain;

namespace RoomMate.Persistence.Repositories
{
    internal class BookingService : IBookingService
    {
        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Save(Booking book)
        {
            throw new NotImplementedException();
        }
    }
}
