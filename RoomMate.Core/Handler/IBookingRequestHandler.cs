using RoomMate.Core.Model;

namespace RoomMate.Core.Handler
{
    public interface IBookingRequestHandler
    {
        BookingResult Book(BookingRequest bookingRequest);   
    }
}