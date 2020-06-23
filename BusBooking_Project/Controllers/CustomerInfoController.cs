using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusBooking_Project.Models;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.CsRepository;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.SupportsTu;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;

namespace BusBooking_Project.Controllers
{
    [Route("customerinfo")]

    public class CustomerInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepo _IAcc;
        private readonly IRoutesRepo _IRou;
        private readonly IBusRePo _IBus;
        private readonly IBookingRepo _IBook;

        public CustomerInfoController(ILogger<HomeController> logger, IAccountRepo iAcc, IRoutesRepo iRou, IBusRePo iBus, IBookingRepo iBook)
        {
            _logger = logger;
            _IAcc = iAcc;
            _IRou = iRou;
            _IBus = iBus;
            _IBook = iBook;
        }

        [HttpGet("index")]
        [HttpGet("")]
        public IActionResult Index([FromQuery] int seat, [FromQuery] string cateprice, [FromQuery] string routeprice, [FromQuery] int RouteId)
        {
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking) == false)
            {
                return RedirectToAction("index", "home");
            }
            BookingView inforBooking = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
            inforBooking.SeatId = seat;
            inforBooking.CategoryPrice = Convert.ToDecimal(cateprice);
            inforBooking.RoutePrice = Convert.ToDecimal(routeprice);
            inforBooking.RouteId = RouteId;
            var route = _IRou.GetRouteById(inforBooking.RouteId);
            inforBooking.BusId = route.BusId;
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking))
            {
                CookieSupport.Remove(HttpContext, CookieSupport.InfoBooking);
                CookieSupport.Set(HttpContext, CookieSupport.InfoBooking, JsonConvert.SerializeObject(inforBooking), DateTime.Now.Minute + 5);

                BookingView inforBooking2 = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
            }
            else
            {
                CookieSupport.Set(HttpContext, CookieSupport.InfoBooking, JsonConvert.SerializeObject(inforBooking), DateTime.Now.Minute + 5);
            }
            return View();
        }

        [HttpPost("saveinfo")]
        public IActionResult SaveInfo(AccountView customer, int Cusage)
        {
            var phoneRegex = new Regex(@"^[0-9]{8,10}$");
            var emailRegex = new Regex(@"^((?!\.)[\w-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking) && Regex.IsMatch(customer.Email, emailRegex.ToString()) && Regex.IsMatch(customer.Phone, phoneRegex.ToString()))
            {
                BookingView infoBooking = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
                customer.Description = AgeCal(Cusage);
                customer.DayCreate = DateTime.Now;
                customer.RoleId = 3;
                var accid = _IAcc.CreateCustomer(customer);
                if (accid > 0)
                {
                    infoBooking.UserId = accid;
                    infoBooking.Code = GenerateCode.RandomPassword(false);

                    infoBooking.Id = _IBook.CreateBooking(infoBooking);           // Create Booking

                    ActionBooking(infoBooking.BusId, "create");

                    CookieSupport.Remove(HttpContext, CookieSupport.InfoBooking);

                    CookieSupport.Set(HttpContext, CookieSupport.InfoBooking, JsonConvert.SerializeObject(infoBooking), DateTime.Now.Minute + 10);
                    return RedirectToAction("bookingsuccess", "customerinfo");
                }
            }

            if (!Regex.IsMatch(customer.Email, emailRegex.ToString()))
            {
                ModelState.AddModelError("Email", "Email is Invalid!!");
            }

            if (!Regex.IsMatch(customer.Phone, phoneRegex.ToString()))
            {
                ModelState.AddModelError("Phone", "Phone is Invalid!!");
            }
            return View("index");
        }

        [NonAction]
        private string AgeCal(int age)
        {
            string strage = null;

            switch (age)
            {
                case 1:    // Children
                    strage = "Age:Children";
                    break;
                case 2:         // Young
                    strage = "Age:Young";
                    break;
                case 4:         // Old
                    strage = "Age:Adults";
                    break;
                default:        // Middle-age
                    strage = "Age:Middle-age";
                    break;
            }
            return strage;

        }


        [HttpGet("bookingsuccess")]
        public IActionResult BookingSuccess()
        {
            BookingView infoBooking = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
            if (infoBooking.Id != 0)
            {
                var infbook = _IBook.GetInfoBooking(infoBooking.Id);
                infbook.BusId = infoBooking.BusId;
                ViewBag.d = infbook;
                return View();
            }
            return RedirectToAction("index", "home");
        }

        [HttpGet("cancel")]
        public IActionResult Cancel([FromQuery] int idBook, [FromQuery] int busid)
        {
            var rs = _IBook.Delete(idBook).Result;
            if (rs)
            {
                ActionBooking(busid, "cancel");
                return RedirectToAction("index", "home");
            }
            else
            {
                return RedirectToAction("bookingsuccess", "customerinfo");
            }
        }

        private void ActionBooking(int BusId, string action)
        {
            switch (action.ToLower().Trim())
            {
                case "create":
                    _IBus.UpdateSeatEmpty(BusId, 1, "tru");
                    break;

                case "cancel":
                    _IBus.UpdateSeatEmpty(BusId, 1, "cong");
                    break;
            }
        }
    }
}
