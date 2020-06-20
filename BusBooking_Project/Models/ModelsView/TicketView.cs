using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class TicketView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public int RoutesId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
        public string Code { get; set; }
        public string NameUser { get; set; }
        public string CodeSeat { get; set; }
        public string CodeBus { get; set; }
        public string TimeGo { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
