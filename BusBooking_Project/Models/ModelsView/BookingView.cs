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
        public string Code { get; set; }

        public int StationFrom { get; set; }
        public int StationTo { get; set; }
        public string StationNameFrom { get; set; }
        public string StationNameTo { get; set; }
        public decimal CategoryPrice { get; set; }
        public decimal RoutePrice { get; set; }
        public string BusCode { get; set; }
        public string SeatCode { get; set; }
        public string ClientName { get; set; }
        public string ClientDescription { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }

    }
}