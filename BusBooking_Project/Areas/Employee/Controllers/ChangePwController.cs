using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Supports;

namespace BusBooking_Project.Areas.Employee.Controllers
{
    [Authorize(Roles = "E", AuthenticationSchemes = "SCHEME_EMP")]
    [Area("employee")]
    [Route("employee/changepw")]

    public class ChangePwController : Controller
    {
        private readonly ILogger<ChangePwController> _logger;
        private readonly IAccountRepo accountRepository;
        private IConfiguration configuration;

        public ChangePwController(
            ILogger<ChangePwController> logger,
            IAccountRepo _accountRepository,
            IConfiguration _configuration)
        {
            _logger = logger;
            accountRepository = _accountRepository;
            configuration = _configuration;
        }
        [Route("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("index")]
        public IActionResult Index(AccountView accountView, string oldpw)
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString().Trim());
            var account = accountRepository.GetByIdACE(id);
            if(oldpw != null)
            {
                string pwhash = MD5ACE.Encrypt(oldpw);
                if (pwhash.Equals(account.Password))
                {
                    if (accountRepository.UpdatePassword(accountView))
                    {
                        ViewBag.rs = "1";
                        return RedirectToAction("login", "employee");
                    }
                    else
                    {
                        ViewBag.rs = "0";
                    }
                }
            } else
            {
                ViewBag.rs = "0";                
            }
            return View();
        }
    }
}
