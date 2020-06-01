using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Supports;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/user")]
    public class UserAdminController : Controller
    {
        #region ctor
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<UserAdminController> logger;
        private readonly IAccountRepo accountRepository;

        public UserAdminController(
            IWebHostEnvironment _webHostEnvironment,
            ILogger<UserAdminController> _logger,
            IAccountRepo _accountRepository)
        {
            webHostEnvironment = _webHostEnvironment;
            logger = _logger;
            accountRepository = _accountRepository;
        }
        #endregion


        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<AccountView> list = accountRepository.GetData(page);
            ViewBag.Rows = accountRepository.CountData();
            return View(list);
        }


        [HttpGet("search")]
        public IActionResult Search()
        {
            int propertySearch = Convert.ToInt32(HttpContext.Request.Query["propertysearch"].ToString().Trim().ToLower());
            string textsearch = HttpContext.Request.Query["textsearch"].ToString().Trim().ToLower();
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<AccountView> listUser = new List<AccountView>();
            switch (propertySearch)
            {
                case (int)SearchUser.Code:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Code);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Code);
                    break;
                case (int)SearchUser.Name:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Name);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Name);
                    break;
                case (int)SearchUser.Email:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Email);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Email);
                    break;
                case (int)SearchUser.Phone:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Phone);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Phone);
                    break;
                case (int)SearchUser.Address:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Address);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Address);
                    break;
                default:
                    listUser = accountRepository.Search(page, textsearch, (int)SearchUser.Code);
                    ViewBag.Rows = accountRepository.CountSearchData(textsearch, (int)SearchUser.Code);
                    break;
            }
            return View("index", listUser);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("AddEdit", new Account());
        }

        [HttpGet("modify/{id}")]
        public IActionResult Modify(int id)
        {
            Account account = accountRepository.GetById(id).Result;
            return View("AddEdit", account);
        }


        [HttpGet("remove")]
        public IActionResult Remove(string listID)
        {
            try
            {
                string[] ids = JsonConvert.DeserializeObject<string[]>(listID);
                bool check = true;
                ids.ToList().ForEach(s =>
                {
                    if (!accountRepository.SetStatus(Convert.ToInt32(s)))
                        check = false;
                });
                if (!check) return Json("");
                return Json("1");
            }
            catch
            {
                return Json("");
            }
        }

        [HttpGet("active")]
        public IActionResult Active(int id)
        {
            if (accountRepository.SetActive(id))
            {
                return Json("200");
            }
            return Json("500");
        }
    }
}
