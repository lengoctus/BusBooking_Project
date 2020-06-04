using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public int SpacingId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
        public string Code { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Spacing Spacing { get; set; }
    }
}
