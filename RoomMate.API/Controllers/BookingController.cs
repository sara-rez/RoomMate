using Microsoft.AspNetCore.Mvc;
using RoomMate.Core.Handler;
using RoomMate.Core.Model;

namespace RoomMate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingRequestHandler bookingRequestHandler;

        public BookingController(IBookingRequestHandler bookingRequestHandler)
        {
            this.bookingRequestHandler = bookingRequestHandler;
        }

        public IActionResult Book(BookingRequest request)
        {
            if(ModelState.IsValid)
            {
                var result = bookingRequestHandler.Book(request);
                if(result.Flag == Core.Enum.BookingResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(BookingRequest.Date), "No rooms available for this date");
            }

            return BadRequest(ModelState);
        }      
    }
}
