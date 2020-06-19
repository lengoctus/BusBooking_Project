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

        public SelectedSeatController(ILogger<SelectedSeatController> logger, IRoutesRepo iRou, ICategoryRepo iCate)
        {
            _logger = logger;
            _IRou = iRou;
            _ICate = iCate;
        }

        [Route("index")]
        public IActionResult Index()
        {
            if (CookieSupport.CheckCookieExists(HttpContext, CookieSupport.InfoBooking) == false)
            {
                return RedirectToAction("index", "Home");
            }
            //int CateId = Convert.ToInt32(HttpContext.Request.Query["CateId"]);
            //if (CateId > 0)
            //{
            //    LoadData(CateId);
            //}
            //else
            //{
            //}
                LoadData(0);

            return View();
        }

        [HttpPost("getlistseat")]
        public IActionResult GetListSeat([FromBody] string Timevalue)
        {

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
            string[] arrInfo = JsonConvert.DeserializeObject<string[]>(HttpContext.Request.Cookies[CookieSupport.InfoBooking]);
            // Lay danh sach Category
            var listcate = _IRou.GetCateogryByFromTo(Convert.ToInt32(arrInfo[0]), Convert.ToInt32(arrInfo[2]));
            //  Lay danh sach tuyen duong theo diem di va den va loai xe
            var listRoutes = new List<RoutesView>();
            if (CateId > 0)
            {
                listRoutes = _IRou.GetRoutesByFromTo(Convert.ToInt32(arrInfo[0]), Convert.ToInt32(arrInfo[2]), CateId).ToList();
            }
            else
            {
               listRoutes = _IRou.GetRoutesByFromTo(Convert.ToInt32(arrInfo[0]), Convert.ToInt32(arrInfo[2]), listcate[CateId].CateId).ToList();
            }



            ViewBag.infoBooking = arrInfo;
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



            //from[0]: "20"
            //name[1]: "tp Ho chi minh - quan 1".
            //To[2]: "22"
            //name[3]: "nghe an - tp vinh"
            //time[4]: "26-06-2020"
            //qty[5]: 1
            //price[6]: 50000

        }

    }
}
