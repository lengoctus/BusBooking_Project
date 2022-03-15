using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Employee.Controllers
{
    [Authorize(Roles = "E", AuthenticationSchemes = "SCHEME_EMP")]
    [Area("employee")]
    [Route("employee/sendmail")]

    public class SendMailController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
