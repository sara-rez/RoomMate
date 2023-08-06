using Moq;
using RoomMate.Core.Enum;
using RoomMate.Core.Handler;
using RoomMate.Core.Interfaces;
using RoomMate.Core.Model;
using RoomMate.Domain.Domain;

namespace RoomMate.Core.Test
{
    public class BookingRequestHandlerTest
    {
        private readonly BookingRequestHandler _handler;
        private readonly BookingRequest _request;
        private Mock<IBookingRepository> _bookingRepositoryMock;
        private List<Room> _availableRooms;

        public BookingRequestHandlerTest()
        {
            // Arrange
            _request = new BookingRequest
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "test@test.com",
                Date = DateTime.Now
            };

            _availableRooms = new List<Room> { new Room() { Id = 1 } };
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _bookingRepositoryMock.Setup(q => q.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);

            _handler = new BookingRequestHandler(_bookingRepositoryMock.Object);
        }

        [Fact]
        public void Book_WhenCalled_Should_Return_Response_With_Values_Of_Request()
        {
            // Act
            var result = _handler.Book(_request);

            // Assert
            result.ShouldNotBeNull();

            result.FirstName.ShouldBe(_request.FirstName);
            result.LastName.ShouldBe(_request.LastName);
            result.Email.ShouldBe(_request.Email);
        }

        [Fact]
        public void Book_WhenCalled_Should_Throw_Exception_For_Null_Request()
        {
            // Act
            Action action = () => _handler.Book(null);

            // Assert
            var exception = Should.Throw<ArgumentNullException>(action);
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Book_WhenCalled_Should_Save_Booking_Request()
        {
            // Arrange
            Booking? savedBooking = null;
            _bookingRepositoryMock.Setup(x => x.Save(It.IsAny<Booking>()))
                .Callback<Booking>(booking =>
                {
                    savedBooking = booking;
                });

            // Act
            _handler.Book(_request);

            // Assert
            _bookingRepositoryMock.Verify(x => x.Save(It.IsAny<Booking>()), Times.Once);
          
            savedBooking.ShouldNotBeNull();

            savedBooking.FirstName.ShouldBe(_request.FirstName);
            savedBooking.LastName.ShouldBe(_request.LastName);
            savedBooking.Email.ShouldBe(_request.Email);
            savedBooking.RoomID.ShouldBe(_availableRooms.First().Id);
        }

        [Fact]
        public void Book_WhenCalled_Should_Not_Save_Booking_If_None_Available()
        {
            // Arrange
            _availableRooms.Clear();

            // Act
            _handler.Book(_request);

            // Assert
            _bookingRepositoryMock.Verify(x => x.Save(It.IsAny<Booking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Success, true)]
        [InlineData(BookingResultFlag.Failure, false)]
        public void Book_WhenCalled_Sould_Return_SuccessOrFailure_In_Result(BookingResultFlag bookingResultFlag, bool isAvailable)
        {
            // Arrange
            if(!isAvailable)
            {
                _availableRooms.Clear();
            }

            // Act
            var result = _handler.Book(_request);

            // Assert
            result.Flag.ShouldBe(bookingResultFlag);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Book_WhenCalled_Should_Return_BookingId_In_Result(int? bookingId, bool isAvailable)
        {
            // Arrange
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            _bookingRepositoryMock.Setup(x => x.Save(It.IsAny<Booking>()))
              .Callback<Booking>(booking =>
              {
                  booking.Id = bookingId;
              });

            // Act
            var result = _handler.Book(_request);

            // Assert
            result.BookingId.ShouldBe(bookingId);
        }
    }
}
