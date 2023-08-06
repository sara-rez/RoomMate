using RoomMate.Domain.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace RoomMate.Domain.Domain
{
    public class Booking : BookingBase
    {
        public int? Id { get; set; }
        public int RoomID { get; set; }
        public Room Room { get; set; }
    }
}