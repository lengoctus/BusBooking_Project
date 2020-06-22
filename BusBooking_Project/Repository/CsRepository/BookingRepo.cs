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
        private readonly ConnectDbContext _db;
        public BookingRepo(ConnectDbContext db) : base(db)
        {
            _db = db;
        }

        public override bool CheckIsExists(Booking entity)
        {
            var d = entity.DayCreate.ToString("dd/MM/yyyy");
            try
            {
                var check = GetAll().Result.FirstOrDefault(p => p.DayCreate.Day == entity.DayCreate.Day && p.DayCreate.Month == entity.DayCreate.Month && p.DayCreate.Year == entity.DayCreate.Year && p.RouteId == entity.RouteId && p.BusId == entity.BusId && p.SeatId == entity.SeatId);
                if (check != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }

        #region Create Booking
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
                SeatId = bookingView.SeatId,
                Code = bookingView.Code
            };

            return Create(booking, CheckIsExists(booking)).Result.Id;
        }
        #endregion

        #region Get Info Booking
        public BookingView GetInfoBooking(int bookingId)
        {
            try
            {
                var booking = GetAll().Result.Where(p => p.Id == bookingId).Join(_db.Bus, book => book.BusId, bus => bus.Id, (book, bus) => new BookingView
                {
                    Id = book.Id,
                    Code = book.Code,
                    BusCode = bus.Code,
                    DayCreate = book.DayCreate,
                    DayStart = book.DayStart,
                    Active = book.Active,
                    SeatId = book.SeatId,
                    UserId = book.UserId,
                    CategoryPrice = bus.Category.Price,
                    RoutePrice = book.Route.Price,
                    StationFrom = book.Route.StationFrom,
                    StationTo = book.Route.StationTo
                }).Join(_db.Seat, book => book.SeatId, seat => seat.Id, (book, seat) => new BookingView
                {
                    Id = book.Id,
                    Code = book.Code,
                    BusCode = book.BusCode,
                    DayCreate = book.DayCreate,
                    DayStart = book.DayStart,
                    Active = book.Active,
                    SeatId = book.SeatId,
                    UserId = book.UserId,
                    SeatCode = seat.Code,
                    CategoryPrice = book.CategoryPrice,
                    RoutePrice = book.RoutePrice,
                    StationFrom = book.StationFrom,
                    StationTo = book.StationTo
                }).Join(_db.Account, book => book.UserId, acc => acc.Id, (book, acc) => new BookingView
                {
                    Id = book.Id,
                    Code = book.Code,
                    BusCode = book.BusCode,
                    DayCreate = book.DayCreate,
                    DayStart = book.DayStart,
                    Active = book.Active,
                    SeatId = book.SeatId,
                    UserId = book.UserId,
                    ClientPhone = acc.Phone,
                    ClientEmail = acc.Email,
                    SeatCode = book.SeatCode,
                    ClientName = acc.Name,
                    ClientDescription = acc.Description,
                    CategoryPrice = book.CategoryPrice,
                    RoutePrice = book.RoutePrice,
                    StationFrom = book.StationFrom,
                    StationTo = book.StationTo
                }).Join(_db.Station, book => book.StationFrom, sta => sta.Id, (book, sta) => new BookingView
                {
                    Id = book.Id,
                    Code = book.Code,
                    BusCode = book.BusCode,
                    DayCreate = book.DayCreate,
                    DayStart = book.DayStart,
                    Active = book.Active,
                    SeatId = book.SeatId,
                    UserId = book.UserId,
                    SeatCode = book.SeatCode,
                    ClientPhone = book.ClientPhone,
                    ClientEmail = book.ClientEmail,
                    ClientName = book.ClientName,
                    ClientDescription = book.ClientDescription,
                    CategoryPrice = book.CategoryPrice,
                    RoutePrice = book.RoutePrice,
                    StationNameFrom = sta.Name,
                    StationTo = book.StationTo
                }).Join(_db.Station, book => book.StationTo, sta => sta.Id, (book, sta) => new BookingView
                {
                    Id = book.Id,
                    Code = book.Code,
                    BusCode = book.BusCode,
                    DayCreate = book.DayCreate,
                    DayStart = book.DayStart,
                    ClientName = book.ClientName,
                    ClientEmail = book.ClientEmail,
                    ClientPhone = book.ClientPhone,
                    Active = book.Active,
                    SeatId = book.SeatId,
                    UserId = book.UserId,
                    SeatCode = book.SeatCode,
                    ClientDescription = book.ClientDescription.Substring(4, book.ClientDescription.Length),
                    CategoryPrice = book.CategoryPrice,
                    RoutePrice = book.RoutePrice,
                    StationNameFrom = book.StationNameFrom,
                    StationNameTo = sta.Name
                }).FirstOrDefault();
                return booking;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
