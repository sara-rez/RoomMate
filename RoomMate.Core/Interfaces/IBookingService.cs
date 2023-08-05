using RoomMate.Domain.Domain;

namespace RoomMate.Core.Interfaces
{
    public interface IBookingService
    {
        void Save(Booking book);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}
