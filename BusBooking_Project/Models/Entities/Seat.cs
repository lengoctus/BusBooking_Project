using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Seat
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string Code { get; set; }
        public bool? Status { get; set; }

        public virtual Bus Bus { get; set; }
    }
}
