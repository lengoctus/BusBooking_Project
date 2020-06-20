using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Routes
    {
        public Routes()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int StationFrom { get; set; }
        public int StationTo { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public TimeSpan TimeGo { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public int BusId { get; set; }
        public string TimeRun { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }
    }
}
