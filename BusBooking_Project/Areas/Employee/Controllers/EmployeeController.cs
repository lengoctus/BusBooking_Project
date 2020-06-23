using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Supports;

namespace BusBooking_Project.Areas.Employee.Controllers
{
    [AllowAnonymous]
    [Area("employee")]
    [Route("employee")]
    public class EmployeeController : Controller
    {   //phần của sơn login thành công
        private readonly ILogger<EmployeeController> _logger;
        private readonly IAccountRepo accountRepository;
        private IConfiguration configuration;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IAccountRepo _accountRepository,
            IConfiguration _configuration)
        {
            _logger = logger;
            accountRepository = _accountRepository;
            configuration = _configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(AccountView accountView)
        {
            if (accountView != null)
            {
                if (CheckLogin(accountView))
                {
                    return Json("200");
                }
                return Json("500");
            }
            return View();
        }

        private bool CheckLogin(AccountView accountView)
        {
            try
            {
                AccountView accountLogin = accountRepository.Login(new Account { Email = accountView.Email, Password = accountView.Password });
                if (accountLogin == null)
                {
                    return false;
                }
                SercurityManagerACE.Login(HttpContext, accountLogin, "SCHEME_EMP");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerACE.Logout(HttpContext, "SCHEME_EMP");
            return RedirectToAction("login", "employee");
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
        //phần của sơn
    }

}
