﻿using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IBookingRepo : IGenericRepo<Booking>
    {
        int CreateBooking(BookingView bookingView);
        List<BookingView> GetAllInfoBooking();
        BookingView GetInfoBooking(int UserId);
        BookingView GetInfoBookingClient(string email, string phone, string bookingCode);
        bool UpdateActive(int bookId);
    }
}
