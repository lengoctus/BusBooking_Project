using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAccountRepo _IAcc;

        public AdminController(ILogger<AdminController> logger, IAccountRepo iAcc)
        {
            _logger = logger;
            _IAcc = iAcc;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(AccountView accountView)
        {
            AccountView accountView1 = _IAcc.Login(new Models.Entities.Account { Email = accountView.Email, Password = accountView.Password });
            if (accountView1 == null)
            {
                return Json("500");
            }
            return Json("200");
        }
    }
}
