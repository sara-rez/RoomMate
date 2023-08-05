using RoomMate.Domain.BaseModels;

namespace RoomMate.Domain.Domain
{
    public class Booking : BookingBase
    {       
        public int RoomID { get; set; }
        public int? Id { get; set; }
        public Room Room { get; set; }
    }
}