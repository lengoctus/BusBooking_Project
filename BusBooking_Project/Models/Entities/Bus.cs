using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Bus
    {
        public Bus()
        {
            Booking = new HashSet<Booking>();
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public string Image { get; set; }
        public int TotalSeat { get; set; }
        public int SeatEmpty { get; set; }
        public int CateId { get; set; }

        public virtual Category Cate { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
