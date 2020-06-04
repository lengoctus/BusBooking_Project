using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public string Code { get; set; }
    }
}
