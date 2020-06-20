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
using System.Net;
using Newtonsoft.Json;

namespace BusBooking_Project.Controllers
{
    [Route("selectedSeat")]
    public class SelectedSeatController : Controller
    {
        private readonly ILogger<SelectedSeatController> _logger;
        private readonly IRoutesRepo _IRou;
        private readonly ICategoryRepo _ICate;
        private readonly ISeatRePo _ISeat;

        public SelectedSeatController(ILogger<SelectedSeatController> logger, IRoutesRepo iRou, ICategoryRepo iCate, ISeatRePo iSeat)
        {
            _logger = logger;
            _IRou = iRou;
            _ICate = iCate;
            _ISeat = iSeat;
        }

        [Route("index")]
        public IActionResult Index()
        {
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking) == false)
            {
                return RedirectToAction("index", "Home");
            }
            LoadData(0);

            return View();
        }

        [HttpPost("getlistseat")]
        public IActionResult GetListSeat([FromBody] string Timevalue)
        {
            if (!string.IsNullOrEmpty(Timevalue))
            {
                int RouteId = Convert.ToInt32(Timevalue);
                var Route = _IRou.GetRouteById(RouteId);

                var listSeat = _ISeat.GetAllByBusId(Route.BusId);
                return Json(listSeat);
            }
            return Json("0");
        }

        [HttpPost("getseatbycategory")]
        public IActionResult GetSeatByCategory(int pickUpPoint)
        {
            if (pickUpPoint > 0 && CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking) == true)
            {
                ViewBag.valueCategory = pickUpPoint;
                LoadData(pickUpPoint);
                return View("Index");
            }
            return RedirectToAction("index", "home");
        }

        [NonAction]
        public void LoadData(int CateId)
        {
            BookingView inforBooking = JsonConvert.DeserializeObject<BookingView>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
            // Lay danh sach Category
            var listcate = _IRou.GetCateogryByFromTo(inforBooking.StationFrom, inforBooking.StationTo);
            //  Lay danh sach tuyen duong theo diem di va den va loai xe
            var listRoutes = new List<RoutesView>();
            if (CateId > 0)
            {
                listRoutes = _IRou.GetRoutesByFromTo(inforBooking.StationFrom, inforBooking.StationTo, CateId).ToList();
            }
            else
            {
                listRoutes = _IRou.GetRoutesByFromTo(inforBooking.StationFrom, inforBooking.StationTo, listcate[CateId].CateId).ToList();
            }

            ViewBag.infoBooking = inforBooking;
            ViewBag.listcate = listcate;


            // Gia xe theo tuyen duong
            ViewBag.RoutePrice = listRoutes.FirstOrDefault().Price;

            // Danh sach thoi gian di
            ViewBag.listRouteTimeGoRun = listRoutes.Select(p => new RoutesView
            {
                Id = p.Id,
                TimeGo = p.TimeGo,
                TimeRun = p.TimeRun
            }).ToList();

            var Route = _IRou.GetRouteById(ViewBag.listRouteTimeGoRun[0].Id);

            ViewBag.listSeat = _ISeat.GetAllByBusId(Route.BusId);
        }

    }
}
