using RoomMate.Core.Enum;
using RoomMate.Core.Interfaces;
using RoomMate.Core.Model;
using RoomMate.Domain.BaseModels;
using RoomMate.Domain.Domain;

namespace RoomMate.Core.Handler
{
    public class BookingRequestHandler : IBookingRequestHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingRequestHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public BookingResult Book(BookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }
            var result = CreateBookingObject<BookingResult>(bookingRequest);

            var avaiableRooms = _bookingRepository.GetAvailableRooms(bookingRequest.Date);
            if (avaiableRooms.Any())
            {
                var room = avaiableRooms.First();
                var booking = CreateBookingObject<Booking>(bookingRequest);
                booking.RoomID = room.Id;

                _bookingRepository.Save(booking);

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