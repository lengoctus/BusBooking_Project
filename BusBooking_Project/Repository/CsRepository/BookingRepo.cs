using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class BookingRepo : GenericRepo<Booking>, IBookingRepo
    {
        public BookingRepo(ConnectDbContext db) : base(db)
        {
        }

        public override bool CheckIsExists(Booking entity)
        {
            throw new NotImplementedException();
        }

        public int CreateBooking(BookingView bookingView)
        {
            var booking = new Booking
            {
                DayCreate = DateTime.Now,
                DayStart = bookingView.DayStart,
                RouteId = bookingView.RouteId,
                UserId = bookingView.UserId,
                QuantityTicket = bookingView.QuantityTicket,
                Active = false,
                BusId = bookingView.BusId,
                SeatId = bookingView.BusId,
                Code = bookingView.Code
            };

            return Create(booking, false).Result.Id;
        }
    }
}
