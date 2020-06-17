using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class BookingView : IEntity
    {
        public int Id { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayStart { get; set; }
        public int RouteId { get; set; }
        public int UserId { get; set; }
        public int QuantityTicket { get; set; }
        public bool Active { get; set; }
        public int BusId { get; set; }
        public int SeatId { get; set; }

    }
}
