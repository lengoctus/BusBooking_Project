using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Supports;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/bus")]

    public class BusAdminController : Controller
    {
        private readonly IBusRePo _IBusrepo;
        private readonly ICategoryRepo _ICategoryrepo;

        private readonly IWebHostEnvironment webHostEnvironment;

        public BusAdminController(IWebHostEnvironment webHostEnvironment, IBusRePo iBusrepo, ICategoryRepo iCategoryRepo)
        {
            this.webHostEnvironment = webHostEnvironment;
            _IBusrepo = iBusrepo;
            _ICategoryrepo = iCategoryRepo;
        }

        [Route("")]
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            if (TempData["ModifySuccess"] != null)
            {
                ViewBag.ModifySuccess = CheckError.Success;
            }
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            ViewBag.cs = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList().Where(p => p.Status == true).ToList();
            List<BusView> listBus = _IBusrepo.GetAllBus(page);
            ViewBag.Rows = _IBusrepo.CountAllBus();
            return View(listBus);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.categories = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View(new BusView {Active = true });
        }

        [HttpPost("create")]
        public IActionResult Create(BusView busView, IFormFile inputphoto)
        {
            busView.Status = true;
            string FileNameSave = "dui.jpg";
            if (inputphoto != null)
            {
                FileNameSave = FileACE.SaveFile(webHostEnvironment, inputphoto, "admin/image");
            }
            busView.Image = FileNameSave;
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                id = _IBusrepo.CreateACE(busView);

            }
            switch (id)
            {
                case (int)CheckError.AlreadyCode:
                    ViewBag.Result = CheckError.AlreadyCode;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    return RedirectToAction("index");
            }
            ViewBag.categories = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View(busView);
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit()
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            BusView busView = _IBusrepo.GetByIdBus(id);
            ViewBag.categories = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View(busView);
        }

        [HttpPost("edit")]
        public IActionResult Edit(BusView busView, IFormFile inputphoto)
        {
            busView.Status = true;
            busView.Active = true;
            string FileNameSave = "dui.jpg";
            if (inputphoto != null)
            {
                FileNameSave = FileACE.SaveFile(webHostEnvironment, inputphoto, "admin/image");
                FileACE.RemoveFile(webHostEnvironment, $"admin\\image\\{_IBusrepo.GetByIdBus(busView.Id).Image}");
            }
            busView.Image = FileNameSave;
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                id = _IBusrepo.UpdateBus(busView);
            }
            switch (id)
            {
                case (int)CheckError.AlreadyCode:
                    ViewBag.Result = CheckError.AlreadyCode;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    TempData["ModifySuccess"] = CheckError.Success;
                    return RedirectToAction("index");
            }
            ViewBag.categories = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View(busView);
        }

        [HttpGet("active")]
        public IActionResult Active(int id)
        {
            if (_IBusrepo.SetActive(id))
            {
                return Json("200");
            }
            return Json("500");
        }

        [HttpGet("search")]
        public IActionResult Search()
        {
            int propertySearch = Convert.ToInt32(HttpContext.Request.Query["propertysearch"].ToString().Trim().ToLower());
            string textsearch = HttpContext.Request.Query["textsearch"].ToString().Trim().ToLower();
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<BusView> listBus = new List<BusView>();
            switch (propertySearch)
            {
                case (int)SearchBus.Code:
                    listBus = _IBusrepo.Search(page, textsearch, (int)SearchBus.Code);
                    ViewBag.Rows = _IBusrepo.CountSearchData(textsearch, (int)SearchBus.Code);
                    break;

                default:
                    listBus = _IBusrepo.Search(page, textsearch, (int)SearchBus.Code);
                    ViewBag.Rows = _IBusrepo.CountSearchData(textsearch, (int)SearchBus.Code);
                    break;
            }
            ViewBag.cs = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View("index", listBus);
        }

        [HttpGet]
        [Route("searchbycategory")]
        public IActionResult SearchByCategory()
        {
            int id= Convert.ToInt32(HttpContext.Request.Query["id"].ToString().Trim());
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            var seatsbycate = _IBusrepo.SearchByCategory(page, id);
            ViewBag.Rows = _IBusrepo.CountSearchByCategory(id);
            ViewBag.cs = _ICategoryrepo.GetDataACE().Where(p => p.Status == true).ToList();
            return View("index", seatsbycate);
        }
        // thêm hàm remove bus
        [HttpGet("remove")]
        public IActionResult Remove(string listID)
        {
            try
            {
                string[] ids = JsonConvert.DeserializeObject<string[]>(listID);
                bool check = true;
                ids.ToList().ForEach(s =>
                {
                    if (!_IBusrepo.SetStatus(Convert.ToInt32(s)))
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
    }
}
