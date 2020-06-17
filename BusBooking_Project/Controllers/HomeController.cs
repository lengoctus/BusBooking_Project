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
            return View();
        }

        [HttpPost("getrouteto")]
        public IActionResult GetRouteTo([FromBody]int idRouteFrom)
        {
            int idFrom = Convert.ToInt32(idRouteFrom);
            if (idFrom > 0)
            {
                var d = _IRou.GetRoutesTo(idFrom);
                return Json(d);
            }
            return Json("0");
        }

    }
}
