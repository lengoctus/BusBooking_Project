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
        [Route("index")]
        public IActionResult Index()
        {
            if (TempData["ModifySuccess"] != null)
            {
                ViewBag.ModifySuccess = CheckError.Success;
            }
            List<BusView> listBus = _IBusrepo.GetAllBus();
            return View("Index", listBus);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.categories = _ICategoryrepo.GetDataACE();
            return View(new BusView { Image = "abc.jpg", Active = true });
        }

        [HttpPost("create")]
        public IActionResult Create(BusView busView, IFormFile inputphoto)
        {
            busView.Status = true;
            string FileNameSave = "abc.jpg";
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
                case (int)CheckError.AlreadyName:
                    ViewBag.Result = CheckError.AlreadyName;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    return RedirectToAction("index");
            }
            ViewBag.categories = _ICategoryrepo.GetDataACE();
            return View(busView);
        }

        [HttpGet]
        [Route("edit")]
        public IActionResult Edit()
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            BusView busView = _IBusrepo.GetByIdBus(id);
            ViewBag.categories = _ICategoryrepo.GetDataACE();
            return View(busView);

        }

        [HttpPost("edit")]
        public IActionResult Edit(BusView busView, IFormFile inputphoto)
        {
            busView.Status = true;
            busView.Active = true;
            string FileNameSave = "abc.jpg";
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
                case (int)CheckError.AlreadyName:
                    ViewBag.Result = CheckError.AlreadyName;
                    break;
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
            ViewBag.categories = _ICategoryrepo.GetDataACE();
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
            //string strPage = HttpContext.Request.Query["page"].ToString();
            // int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<BusView> listBus = new List<BusView>();
            switch (propertySearch)
            {
                case (int)SearchBus.Code:
                    listBus = _IBusrepo.Search(textsearch, (int)SearchBus.Code);
                    //ViewBag.Rows = _IBusrepo.CountSearchData(textsearch, (int)SearchBus.Code);
                    break;
                case (int)SearchBus.Name:
                    listBus = _IBusrepo.Search(textsearch, (int)SearchBus.Name);
                    //ViewBag.Rows = _IBusrepo.CountSearchData(textsearch, (int)SearchBus.Name);
                    break;
                default:
                    listBus = _IBusrepo.Search(textsearch, (int)SearchBus.Code);
                    //ViewBag.Rows = _IBusrepo.CountSearchData(textsearch, (int)SearchBus.Code);
                    break;
            }
            return View("index", listBus);
        }


    }
}