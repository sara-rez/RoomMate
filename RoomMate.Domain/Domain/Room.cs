using System.ComponentModel.DataAnnotations;

namespace RoomMate.Domain.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}