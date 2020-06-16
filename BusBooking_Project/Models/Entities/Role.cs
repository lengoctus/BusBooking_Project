using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Role
    {
        public Role()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
