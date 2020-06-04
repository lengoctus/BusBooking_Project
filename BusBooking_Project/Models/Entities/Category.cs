using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Category
    {
        public Category()
        {
            Bus = new HashSet<Bus>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Bus> Bus { get; set; }
    }
}
