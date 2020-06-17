using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{

    public class BusView : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public int TotalSeat { get; set; }
        public int SeatEmpty { get; set; }
        public int CateId { get; set; }
        public string CategoryName { get; set; }

    }
}
