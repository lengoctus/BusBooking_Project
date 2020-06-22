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
using System.Globalization;
using System.Net;

namespace BusBooking_Project.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRoutesRepo _IRou;

        public HomeController(ILogger<HomeController> logger, IRoutesRepo iRou)
        {
            _logger = logger;
            _IRou = iRou;
        }

        [Route("index")]
        [HttpGet("~/")]
        public IActionResult Index()
        {
            ViewBag.RoutesFrom = _IRou.GetRoutesFrom();
            ViewBag.RouteTo = _IRou.GetRoutesTo(ViewBag.RoutesFrom[0].StationFrom);
            return View();
        }

        [HttpPost("getrouteto")]
        public IActionResult GetRouteTo([FromBody] int idRouteFrom)
        {
            int idFrom = Convert.ToInt32(idRouteFrom);
            if (idFrom > 0)
            {
                var d = _IRou.GetRoutesTo(idFrom);
                return Json(d);
            }
            return Json("0");
        }

        [HttpGet("gettimeandroutes")]
        public IActionResult GetTimeAndRoutes([FromQuery] int fr, [FromQuery] int to, [FromQuery] string dDate, [FromQuery] int QtyTicket, [FromQuery] string frname, [FromQuery] string toname)
        {
            var InfoBooking = new BookingView
            {
                StationFrom = fr,
                StationTo = to,
                DayStart = DateTime.ParseExact(dDate, "dd-MM-yyyy", null),
                QuantityTicket = QtyTicket,
                StationNameFrom = frname,
                StationNameTo = toname
            };

            DateTime today = DateTime.Now;
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking))
            {
                CookieSupport.Remove(HttpContext, CookieSupport.InfoBooking);
                CookieSupport.Set(HttpContext, CookieSupport.InfoBooking, JsonConvert.SerializeObject(InfoBooking), today.Minute + 5);
            }
            else
            {
                CookieSupport.Set(HttpContext, CookieSupport.InfoBooking, JsonConvert.SerializeObject(InfoBooking), today.Minute + 5);

            }


            return RedirectToAction("index", "selectedSeat");
        }
    }
}
