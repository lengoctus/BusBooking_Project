using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Age
    {
        public Age()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int? FromAge { get; set; }
        public int? FromTo { get; set; }
        public byte? PercentRedu { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
