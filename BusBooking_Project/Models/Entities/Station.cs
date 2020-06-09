using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Station
    {
        public Station()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        public int City { get; set; }
        public int District { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
