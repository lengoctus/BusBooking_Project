using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Controllers
{
    [Route("customer")]

    public class Customer : Controller
    {
        //[Route("~/")]
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
