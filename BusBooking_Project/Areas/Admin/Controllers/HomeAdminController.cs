using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/home")]
    public class HomeAdminController : Controller
    {
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
