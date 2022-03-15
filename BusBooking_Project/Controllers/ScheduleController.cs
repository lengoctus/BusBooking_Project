using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Controllers
{
    [Route("schedule")]
    public class ScheduleController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View("Index");
        }


        [Route("")]
        [Route("schedule_Detail")]
        public IActionResult Schedule_Detail()
        {
            return View("Schedule_Detail");


        }

    }
       
}
