using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Controllers
{
    [Route("ticketinfo")]
    public class TicketInfoController : Controller
    {
        private readonly IBookingRepo _IBook;

        public TicketInfoController(IBookingRepo iBook)
        {
            _IBook = iBook;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("search")]
        public IActionResult Search(string email, string phone, string bookingCode)
        {
            var infoBook = _IBook.GetInfoBookingClient(email, phone, bookingCode);
            return View("Index", infoBook);
        }
    }
}
