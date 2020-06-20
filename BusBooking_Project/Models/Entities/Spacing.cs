using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Spacing
    {
        public Spacing()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int FromSp { get; set; }
        public int ToSp { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public TimeSpan? TimeGo { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public int BusId { get; set; }
        public TimeSpan? TimeRun { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
