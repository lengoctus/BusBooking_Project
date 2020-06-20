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

namespace BusBooking_Project.Controllers
{
    [Route("customerinfo")]

    public class CustomerInfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepo _IAcc;
        private readonly IRoutesRepo _IRou;
        private readonly IBookingRepo _IBook;

        public CustomerInfoController(ILogger<HomeController> logger, IAccountRepo iAcc, IRoutesRepo iRou, IBookingRepo iBook)
        {
            _logger = logger;
            _IAcc = iAcc;
            _IRou = iRou;
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
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking))
            {
                BookingView infoBooking = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
                customer.Description = AgeCal(Cusage);
                var accid = _IAcc.CreateCustomer(customer);
                if (accid > 0)
                {
                    infoBooking.UserId = accid;
                    int bookid = _IBook.CreateBooking(infoBooking);

                }

            }
            return View();
        }


        public string AgeCal(int age)
        {
            string strage = null;
            if (0 < age && age <= 5)
            {
                strage = "Age:Children";
            }
            else if (5 < age && age < 12)
            {
                strage = "Age:Young";
            }
            else if (12 <= age && age < 50)
            {
                strage = "Age:Middle-age";
            }
            else if (age >= 50)
            {
                strage = "Age:Adults";
            }
            return strage;
        }

    }
}
