using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Booking
    {
        public int Id { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayStart { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }
        public int QuantityTicket { get; set; }
        public bool Active { get; set; }
        public int BusId { get; set; }
        public int SeatId { get; set; }
        public string Code { get; set; }

        public virtual Bus Bus { get; set; }
        public virtual Routes Route { get; set; }
        public virtual Account User { get; set; }
    }
}
