using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Controllers
{
    [Route("schedule")]
    public class ScheduleController : Controller
    {
        private readonly IRoutesRepo _IRou;

        public ScheduleController(IRoutesRepo iRou)
        {
            _IRou = iRou;
        }

        [Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var list = _IRou.GetRouteSchedule();
            return View("Index", list);
        }

        [HttpGet("schedule_Detail")]
        public IActionResult Schedule_Detail([FromQuery]int from, [FromQuery]int to)
        {
            var list = _IRou.GetDetailByFromTo(from, to);
            return View("Schedule_Detail", list);


        }

    }
       
}
