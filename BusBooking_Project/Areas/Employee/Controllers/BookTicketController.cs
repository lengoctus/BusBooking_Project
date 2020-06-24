using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.SupportsTu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusBooking_Project.Areas.Employee.Controllers
{   // login thành công
    [Authorize(Roles = "E", AuthenticationSchemes = "SCHEME_EMP")]
    [Area("employee")]
    [Route("employee/bookticket")]
    public class BookTicketController : Controller
    {
        private readonly IBookingRepo _IBook;
        private readonly ITicketRepo _ITicket;
        private readonly IConfiguration _IConf;

        public BookTicketController(IBookingRepo iBook, ITicketRepo iTicket, IConfiguration iConf)
        {
            _IBook = iBook;
            _ITicket = iTicket;
            _IConf = iConf;
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

        [HttpPost("createticket")]
        public IActionResult CreateTicketSendMail(BookingView bookView)
        {
            var rs = _IBook.UpdateActive(bookView.Id);

            var ticketView = new TicketView
            {
                Code = null,
                UserId = bookView.UserId,
                TotalPrice = CalTotalPrice(bookView.ClientDescription, bookView.CategoryPrice, bookView.RoutePrice),
                PaymentStatus = false,
                BookingId = bookView.Id
            };

            var resultCreate = _ITicket.CreateTicket(ticketView);
            bookView.TicketCode = resultCreate.Code;

            return RedirectToAction("sendmail", "bookticket", new { @CusEmail = bookView.ClientEmail, @ticketCode = bookView.TicketCode });
        }

        [HttpGet("sendmail")]
        public IActionResult SendMail(string CusEmail, string ticketCode)
        {
            ViewBag.email = CusEmail;
            ViewBag.ticketCode = ticketCode;
            return View();
        }

        [HttpPost("sendmail")]
        public IActionResult SendMail(string email, string Subject, string contentEmail)
        {

            var mail = new SendMail(_IConf);
            var rs = mail.Send(email, Subject, contentEmail);

            return RedirectToAction("index");

        }

        private decimal CalTotalPrice(string age, decimal categoryPrice, decimal RoutePrice)
        {
            decimal currentPrice = categoryPrice + RoutePrice;
            decimal totalprice;

            switch (age.ToLower())
            {
                case "children":
                    totalprice = currentPrice;
                    break;
                case "young":
                    totalprice = currentPrice + ((50 / 100) * currentPrice);
                    break;
                case "adults":
                    totalprice = currentPrice + ((30 / 100) * currentPrice);
                    break;
                default:
                    totalprice = currentPrice + ((100 / 100) * currentPrice);
                    break;
            }
            return totalprice;

        }


    }
    //login thành công
}
