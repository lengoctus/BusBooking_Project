using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class SeatView : IEntity
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string Code { get; set; }
    }
}
