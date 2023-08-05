using RoomMate.Core.Enum;
using RoomMate.Domain.BaseModels;

namespace RoomMate.Core.Model
{
    public class BookingResult : BookingBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? BookingId { get; set; }
    }
}