using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Route("admin/station")]
    public class StationAdminController : Controller
    {
        #region ctor
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<StationAdminController> logger;
        private readonly IStationRepo stationRepository;
        public StationAdminController(
            IWebHostEnvironment _webHostEnvironment,
            ILogger<StationAdminController> _logger,
            IStationRepo _stationRepository)
        {
            webHostEnvironment = _webHostEnvironment;
            logger = _logger;
            stationRepository = _stationRepository;
        }
        #endregion

        #region index
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            if (TempData["ModifySuccess"] != null)
            {
                ViewBag.ModifySuccess = CheckError.Success;
            }
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<StationView> list = stationRepository.GetData(page);
            ViewBag.Rows = stationRepository.CountData();
            list.ForEach(item =>
            {
                item.CityName = HelperACE.GetDataCity().SingleOrDefault(s => s.Key == item.City).Value;
                item.DistrictName = HelperACE.GetDataDistrict(item.City).SingleOrDefault(s => s.Key == item.District).Value;
            });
            return View(list);
        }
        #endregion

        #region search
        [HttpGet("search")]
        public IActionResult Search()
        {
            int propertySearch = Convert.ToInt32(HttpContext.Request.Query["propertysearch"].ToString().Trim().ToLower());
            string textsearch = HttpContext.Request.Query["textsearch"].ToString().Trim().ToLower();
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            List<StationView> listStation = new List<StationView>();
            switch (propertySearch)
            {
                case (int)SearchStation.Name:
                    listStation = stationRepository.Search(page, textsearch, (int)SearchStation.Name);
                    ViewBag.Rows = stationRepository.CountDataSearch(textsearch, (int)SearchStation.Name);
                    break;
                case (int)SearchStation.Location:
                    listStation = stationRepository.Search(page, textsearch, (int)SearchStation.Location);
                    ViewBag.Rows = stationRepository.CountDataSearch(textsearch, (int)SearchStation.Location);
                    break;
                case (int)SearchStation.Phone:
                    listStation = stationRepository.Search(page, textsearch, (int)SearchStation.Phone);
                    ViewBag.Rows = stationRepository.CountDataSearch(textsearch, (int)SearchStation.Phone);
                    break;
                default:
                    listStation = stationRepository.Search(page, textsearch, (int)SearchStation.Name);
                    ViewBag.Rows = stationRepository.CountDataSearch(textsearch, (int)SearchStation.Name);
                    break;
            }
            return View("index", listStation);
        }
        #endregion

        #region create
        [HttpGet("create")]
        public IActionResult Create()
        {
            // Lấy default HCM + Quận 1
            return View(new StationView { City = 4, District = 9, Active = true }); 
        }

        [HttpPost("create")]
        public IActionResult Create(StationView stationView)
        {
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                string city = HelperACE.GetDataCity()[stationView.City];
                string district = HelperACE.GetDataDistrict(stationView.City)[stationView.District];
                stationView.Location = $"{city} - {district}";
                id = stationRepository.CreateACE(stationView);
            }
            switch (id)
            {
                case (int)CheckError.AlreadyName:
                    ViewBag.Result = CheckError.AlreadyName;
                    break;
                case (int)CheckError.AlreadyPhone:
                    ViewBag.Result = CheckError.AlreadyPhone;
                    break;
                case (int)CheckError.AlreadyLocationCity:
                    ViewBag.Result = CheckError.AlreadyLocationCity;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    return RedirectToAction("index");
            }
            return View(stationView);
        }
        #endregion

        #region view modify
        [HttpGet("getdistric/{cityId}")]
        public IActionResult GetDistrictByCity(int cityId)
        {
            return Json(JsonConvert.SerializeObject(HelperACE.GetDataDistrict(cityId).ToList()));
        }

        [HttpGet("detail")]
        public IActionResult Modify()
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString());
            StationView station = stationRepository.GetByIdACE(id);
            ViewBag.ListDistrict = HelperACE.GetDataDistrict(station.City);
            return View(station);
        }

        [HttpPost("modify")]
        public IActionResult Modify(StationView stationView)
        {
            int id = (int)CheckError.ErrorOrther;
            if (ModelState.IsValid)
            {
                string city = HelperACE.GetDataCity()[stationView.City];
                string district = HelperACE.GetDataDistrict(stationView.City)[stationView.District];
                stationView.Location = $"{city} - {district}";
                id = stationRepository.ModifyACE(stationView);
            }
            switch (id)
            {
                case (int)CheckError.AlreadyName:
                    ViewBag.Result = CheckError.AlreadyName;
                    break;
                case (int)CheckError.AlreadyPhone:
                    ViewBag.Result = CheckError.AlreadyPhone;
                    break;
                case (int)CheckError.ErrorOrther:
                    ViewBag.Result = CheckError.ErrorOrther;
                    break;
                default:
                    TempData["ModifySuccess"] = CheckError.Success;
                    return RedirectToAction("index");
            }
            ViewBag.ListDistrict = HelperACE.GetDataDistrict(stationView.City);
            return View(stationView);
        }
        #endregion

        #region remove
        [HttpGet("remove")]
        public IActionResult Remove(string listID)
        {
            try
            {
                string[] ids = JsonConvert.DeserializeObject<string[]>(listID);
                bool check = true;
                ids.ToList().ForEach(s =>
                {
                    if (!stationRepository.SetStatus(Convert.ToInt32(s)))
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
        #endregion

        #region active
        [HttpGet("active")]
        public IActionResult Active(int id)
        {
            if (stationRepository.SetActive(id))
            {
                return Json("200");
            }
            return Json("500");
        }
        #endregion
    }
}
