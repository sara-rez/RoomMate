using RoomMate.Domain.Domain;

namespace RoomMate.Core.Interfaces
{
    public interface IBookingRepository
    {
        void Save(Booking book);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
    }
}
