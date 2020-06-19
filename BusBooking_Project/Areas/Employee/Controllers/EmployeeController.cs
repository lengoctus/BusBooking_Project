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
    {
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


        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
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
                SercurityManagerACE.Login(HttpContext, accountLogin, "SCHEME_AD");
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
            SercurityManagerACE.Logout(HttpContext, "SCHEME_AD");
            return RedirectToAction("login");
        }

        [HttpGet("forgotpw")]
        public IActionResult ForgotPassword()
        {
            if (TempData["Result"] != null && TempData["Result"].ToString() == "0")
            {
                ViewBag.Result = "OK";
            }
            return View();
        }
        [HttpPost("forgotpw")]
        public IActionResult ForgotPassword(string email)
        {
            string resultCode = accountRepository.InsertCodeForgotPW(email);
            // mail +"-"+ chuỗi mã hoá: khi lấy ra thì slipt cái "-" rồi lấy chuỗi so khớp
            if (resultCode != null)
            {
                string link = "https://localhost:44304/admin/changepw?pwId=" + Convert.ToBase64String(Encoding.ASCII.GetBytes(resultCode));
                if (new SendMailACE(configuration).Send(email, "Change password", "Click the following link: " + link))
                {
                    TempData["Result"] = "0";
                    return RedirectToAction("forgotpassword", "admin", new { area = "admin" });
                }
                else
                {
                    ViewBag.Error = "[Network error please try again later]";
                    return View();
                }
            }
            ViewBag.Error = "[Email not exists. Please check again]";
            return View();
        }

        [HttpGet("changepw")]
        public IActionResult ChangePW()
        {
            try
            {
                string result = Encoding.ASCII.GetString(Convert.FromBase64String(HttpContext.Request.Query["pwId"].ToString()));
                string[] splits = result.Split(new char[] { '-' });
                string email = splits[0];
                string code = splits[1];
                AccountView accountView = accountRepository.CompareCodeChangePW(email, code);
                if (accountView != null)
                {
                    return View(accountView);
                }
            }
            catch { }
            return RedirectToAction("accessDenied");
        }
        [HttpPost("changepw")]
        public IActionResult ChangePW(AccountView account)
        {

            if (accountRepository.UpdatePassword(account))
            {
                return Json("200");
            }
            else
            {
                return Json("500");
            }
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
