using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Employee.Controllers
{
    [Area("employee")]
    [Route("employee")]
    public class EmployeeController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost("login")]
        public IActionResult Login(int id)
        {
            return View("Login");
        }


        [Route("changepw")]
        public IActionResult Changepw()
        {
            return View("Changepw");
        }
        [Route("forgotpw")]
        public IActionResult Forgotpw()
        {
            return View("Forgotpw");
        }
    }

}