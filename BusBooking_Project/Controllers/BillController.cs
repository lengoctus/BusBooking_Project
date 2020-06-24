using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Controllers
{
    [Route("bill")]
    public class BillController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("search")]
        public IActionResult Search() { }
    }
}
