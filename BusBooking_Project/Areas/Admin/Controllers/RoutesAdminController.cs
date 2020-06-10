using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/routes")]
    public class RoutesAdminController : Controller
    {
        private readonly IRoutesRepo _IRout;
        private readonly IStationRepo _IStaion;
        private readonly ICategoryRepo _ICate;

        public RoutesAdminController(IRoutesRepo iRout, IStationRepo iStaion, ICategoryRepo iCate)
        {
            _IRout = iRout;
            _IStaion = iStaion;
            _ICate = iCate;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            ViewBag.StationFrom = _IStaion.GetAllTu();
            ViewBag.Category = _ICate.GetAllCategory();

            return View();
        }

        [HttpGet("search/FromStationId={FromStationId}&ToStationId={ToStationId}&idCategory={idCategory}")]
        public IActionResult Search(int FromStationId, int ToStationId, int idCategory)
        {
            return View();
        }
    }
}