using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryAdminController : Controller
    {
        private readonly ICategoryRepo _CateRepo;

        public CategoryAdminController(ICategoryRepo cateRepo)
        {
            _CateRepo = cateRepo;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]CategoryView categoryView)
        {
            var category = new Category
            {
                Code = categoryView.Code,
                Name = categoryView.Name,
                Active = categoryView.Active
            };

            var check = _CateRepo.CheckIsExists(category);

            if (await _CateRepo.Create(category, check) != null)
            {
                return Json("Success");
            }
            return Json("Error");
        }

    }
}