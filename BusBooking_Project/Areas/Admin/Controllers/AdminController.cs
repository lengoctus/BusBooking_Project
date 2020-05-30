using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Supports;

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
            if (accountView != null)
            {
                AccountView accountView1 = _IAcc.Login(new Account { Email = accountView.Email, Password = accountView.Password });
                if (accountView1 == null)
                {
                    return Json("500");
                }
                return Json("200");
            }
            return View();
        }


        [HttpGet("logout")]
        public IActionResult Logout()
        {
            SercurityManagerCuaSang.Logout(HttpContext, "SCHEME_ADMIN");
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
            //string resultCode = UserBus.ForgorPassword(email); // mail +"-"+chuỗi mã hoá: khi lấy ra thì slipt cái "-" rồi lấy chuỗi so khớp
            //if (resultCode != null)
            //{
            //    string link = "https://localhost:44307/admin/changePW?pwId=" + Convert.ToBase64String(Encoding.ASCII.GetBytes(resultCode));
            //    if (new SendMail(configuration).Send(email, "Change password", "Click the following link: " + link))
            //    {
            //        TempData["Result"] = "0";
            //        return RedirectToAction("forgotpassword", "admin", new { area = "admin" });
            //    }
            //    else
            //    {
            //        ViewBag.Error = "[Network error please try again later]";
            //        return View();
            //    }
            //}
            ViewBag.Error = "[Email invalid. Please check again]";
            return View();
        }

        [HttpGet("changePW")]
        public IActionResult ChangePW()
        {
            try
            {
                //string result = Encoding.ASCII.GetString(Convert.FromBase64String(HttpContext.Request.Query["pwId"].ToString()));
                //string[] splits = result.Split(new char[] { '-' });
                //string email = splits[0];
                //string code = splits[1];
                //UserView userView = UserBus.CompareCodeChangePW(email, code);
                //if (userView != null)
                //{
                //    return View(userView);
                //}
            }
            catch { }
            return RedirectToAction("accessDenied");
        }

        [HttpPost("changePW")]
        public IActionResult ChangePW(AccountView userView)
        {

            //if (UserBus.UpdatePassword(userView))
            //{
            //    return Json("200");
            //}
            //else
            //{
            //    return Json("404");
            //}
            return null;
        }

        [HttpGet("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
