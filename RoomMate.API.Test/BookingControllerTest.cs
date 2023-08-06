using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomMate.API.Controllers;
using RoomMate.Core.Enum;
using RoomMate.Core.Handler;
using RoomMate.Core.Model;

namespace RoomMate.API.Test
{
    public class BookingControllerTest
    {
        private Mock<IBookingRequestHandler> _bookingRequestHandler;
        private BookingController _controller;
        private BookingRequest _request;
        private BookingResult _result;

        public BookingControllerTest()
        {
            _bookingRequestHandler = new Mock<IBookingRequestHandler>();
            _controller = new BookingController(_bookingRequestHandler.Object);
            _request = new BookingRequest();
            _result = new BookingResult();

            _bookingRequestHandler.Setup(x => x.Book(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public void Booking_Should_Call_Booking_Method_If_Valid(int expectedMethodCalls, bool isModelValid, Type expectedActionResultType, BookingResultFlag bookingResultFlag)
        {
            // Arrange
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            _result.Flag = bookingResultFlag;

            // Act
            var result = _controller.Book(_request);

            // Assert
            result.ShouldBeOfType(expectedActionResultType);
            _bookingRequestHandler.Verify(x => x.Book(_request), Times.Exactly(expectedMethodCalls));
        }
    }
}
