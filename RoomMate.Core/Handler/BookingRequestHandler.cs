using RoomMate.Core.Enum;
using RoomMate.Core.Interfaces;
using RoomMate.Core.Model;
using RoomMate.Domain.BaseModels;
using RoomMate.Domain.Domain;

namespace RoomMate.Core.Handler
{
    public class BookingRequestHandler
    {
        private readonly IBookingService _bookingService;

        public BookingRequestHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public BookingResult Book(BookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            var avaiableRooms = _bookingService.GetAvailableRooms(bookingRequest.Date);
            var result = CreateBookingObject<BookingResult>(bookingRequest);
            if (avaiableRooms.Any())
            {
                var room = avaiableRooms.First();
                var booking = CreateBookingObject<Booking>(bookingRequest);
                booking.RoomID = room.Id;

                _bookingService.Save(booking);

                result.BookingId = booking.Id;
                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }

            return result;
        }

        private static TBooking CreateBookingObject<TBooking>(BookingRequest bookingRequest) where TBooking 
            : BookingBase, new()
        {
            return new TBooking
            {
                FirstName = bookingRequest.FirstName,
                LastName = bookingRequest.LastName,
                Email = bookingRequest.Email
            };
        }
    }
}