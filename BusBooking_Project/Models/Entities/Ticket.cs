using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
        public string Code { get; set; }
        public int BookId { get; set; }

        public virtual Account User { get; set; }
    }
}
