using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime? DayCreate { get; set; }
        public DateTime? DayStart { get; set; }
        public int? FromCity { get; set; }
        public int? FromDistrict { get; set; }
        public int? UserId { get; set; }
        public int? QuantityTicket { get; set; }
        public bool? Active { get; set; }
        public int? UserId2 { get; set; }
        public int? BusId { get; set; }
        public int? SeatId { get; set; }
        public int? ToCity { get; set; }
        public int? ToDistrict { get; set; }

        public virtual Bus Bus { get; set; }
        public virtual Account User { get; set; }
        public virtual Account UserId2Navigation { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
