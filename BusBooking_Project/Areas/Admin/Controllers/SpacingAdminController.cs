using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
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
    [Route("admin/spacing")]
    public class SpacingAdminController : Controller
    {

        #region ctor
        private readonly ISpacingRepo spacingRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<SpacingAdminController> logger;

        public SpacingAdminController(
            ISpacingRepo _spacingRepository,
            IWebHostEnvironment _webHostEnvironment,
            ILogger<SpacingAdminController> _logger)
        {
            spacingRepository = _spacingRepository;
            webHostEnvironment = _webHostEnvironment;
            logger = _logger;
        }
        #endregion

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<SpacingView> list = spacingRepository.GetData(page);
            list.ForEach(s =>
            {
                s.BusName = "Bus Name";
            });
            ViewBag.Rows = spacingRepository.CountData();
            return View(list);
        }

        [HttpGet("search")]
        public IActionResult Search()
        {
            int propertySearch = Convert.ToInt32(HttpContext.Request.Query["propertysearch"].ToString().Trim().ToLower());
            string textsearch = HttpContext.Request.Query["textsearch"].ToString().Trim().ToLower();
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<SpacingView> listSpacing = new List<SpacingView>();
            switch (propertySearch)
            {
                case (int)SearchSpacing.FromName:
                    listSpacing = spacingRepository.SearchData(page, textsearch, (int)SearchSpacing.FromName);
                    ViewBag.Rows = spacingRepository.CountSearchData(textsearch, (int)SearchSpacing.FromName);
                    break;
                case (int)SearchSpacing.ToName:
                    listSpacing = spacingRepository.SearchData(page, textsearch, (int)SearchSpacing.ToName);
                    ViewBag.Rows = spacingRepository.CountSearchData(textsearch, (int)SearchSpacing.ToName);
                    break;
                case (int)SearchSpacing.TimeRun:
                    listSpacing = spacingRepository.SearchData(page, textsearch, (int)SearchSpacing.TimeRun);
                    ViewBag.Rows = spacingRepository.CountSearchData(textsearch, (int)SearchSpacing.TimeRun);
                    break;
                case (int)SearchSpacing.TimeGo:
                    listSpacing = spacingRepository.SearchData(page, textsearch, (int)SearchSpacing.TimeGo);
                    ViewBag.Rows = spacingRepository.CountSearchData(textsearch, (int)SearchSpacing.TimeGo);
                    break;
                default:
                    listSpacing = spacingRepository.SearchData(page, textsearch, (int)SearchSpacing.FromName);
                    ViewBag.Rows = spacingRepository.CountSearchData(textsearch, (int)SearchSpacing.FromName);
                    break;
            }
            listSpacing.ForEach(s =>
            {
                s.BusName = "Bus Name";
            });
            return View("index", listSpacing);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View(new SpacingView { Active = true });
        }

        [HttpPost("create")]
        public IActionResult Create(SpacingView spacingView)
        {
            spacingView.Status = true;
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                id = spacingRepository.CreateACE(spacingView);

            }
            switch (id)
            {
                case (int)CheckError.AlreadyStationFrom:
                    ViewBag.Result = CheckError.AlreadyEmail;
                    break;
                case (int)CheckError.AlreadyStationTo:
                    ViewBag.Result = CheckError.AlreadyPhone;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    return RedirectToAction("index");
            }
            return View(spacingRepository);
        }

        [HttpGet("detail")]
        public IActionResult Modify()
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            SpacingView spacing = spacingRepository.GetByIdACE(id);
            spacing.BusName = "Bus Name";
            return View(spacing);
        }

        [HttpPost("modify")]
        public IActionResult Modify(SpacingView spacingView)
        {
            spacingView.Status = true;
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                id = spacingRepository.ModifyACE(spacingView);
            }
            switch (id)
            {
                case (int)CheckError.AlreadyStationFrom:
                    ViewBag.Result = CheckError.AlreadyStationFrom;
                    break;
                case (int)CheckError.AlreadyStationTo:
                    ViewBag.Result = CheckError.AlreadyStationTo;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    TempData["ModifySuccess"] = CheckError.Success;
                    return RedirectToAction("index");
            }
            return View(spacingRepository);
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
                    if (!spacingRepository.SetStatus(Convert.ToInt32(s)))
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
            if (spacingRepository.SetActive(id))
            {
                return Json("200");
            }
            return Json("500");
        }
    }
}
