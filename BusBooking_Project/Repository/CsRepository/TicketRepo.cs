using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.SupportsTu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class TicketRepo : GenericRepo<Ticket>, ITicketRepo
    {
        public TicketRepo(ConnectDbContext db) : base(db)
        {
        }

        public override bool CheckIsExists(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public TicketView CreateTicket(TicketView ticketView)
        {
            var ticket = new Ticket
            {
                Userid = ticketView.UserId,
                TotalPrice = ticketView.TotalPrice,
                PaymentStatus = ticketView.PaymentStatus,
                Code = GenerateCode.RandomPassword(false),
                BookId = ticketView.BookingId
            };

            var result = Create(ticket, false).Result;
            if (result != null)
            {
                ticketView.Id = result.Id;
                ticketView.Code = result.Code;
                return ticketView;
            }
            return null;
        }
    }
}
