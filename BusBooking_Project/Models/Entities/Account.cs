using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Account
    {
        public Account()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Images { get; set; }
        public byte? Gender { get; set; }
        public DateTime? DayCreate { get; set; }
        public DateTime? DayEdited { get; set; }
        public int? EditerId { get; set; }
        public bool? Status { get; set; }
        public byte? RoleId { get; set; }
        public bool? Active { get; set; }
        public string Description { get; set; }
        public string ForgotPass { get; set; }
        public int StationId { get; set; }
        public string Code { get; set; }

        public virtual Role Role { get; set; }
        public virtual Station Station { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
