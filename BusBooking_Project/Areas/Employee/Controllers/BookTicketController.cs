using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Employee.Controllers
{   // login thành công
    [Authorize(Roles = "E", AuthenticationSchemes = "SCHEME_EMP")]
    [Area("employee")]
    [Route("employee/bookticket")]
    public class BookTicketController : Controller
    {
        private readonly IBookingRepo _IBook;

        public BookTicketController(IBookingRepo iBook)
        {
            _IBook = iBook;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            var listBook = _IBook.GetAllInfoBooking();
            return View(listBook);
        }

        [HttpGet("detail")]
        public IActionResult Detail([FromQuery] int bookid)
        {
            var detail = _IBook.GetInfoBooking(bookid);
            if (detail != null)
            {
                return View(detail);
            }
            return View("index");
        }

        [HttpPost("sendmail")]
        public IActionResult Sendmail(BookingView bookView)
        {
            return View(bookView);
        }



    }
    //login thành công
}
