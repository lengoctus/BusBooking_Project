using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Routes
    {
        public Routes()
        {
            Ticket = new HashSet<Ticket>();
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
        public TimeSpan TimeRun { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
