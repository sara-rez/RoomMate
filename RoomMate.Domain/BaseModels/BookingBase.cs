﻿namespace RoomMate.Domain.BaseModels
{
    public abstract class BookingBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}