using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/home")]
    public class HomeAdminController : Controller
    {
        private readonly ICategoryRepo _Cate;

        public HomeAdminController(ICategoryRepo cate)
        {
            _Cate = cate;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("manager")]
        public IActionResult Manager()
        {
            ViewBag.listCate = _Cate.GetAll().Result.Select(p => new CategoryView { 
                Id = p.Id,
                Name = p.Name,
                Active = p.Active,
                Status = p.Status,
                Code = p.Code
            }).ToList();
            return View();
        }
    }
}
