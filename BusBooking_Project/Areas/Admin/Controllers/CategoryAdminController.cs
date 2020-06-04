using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            if (categoryView != null)
            {
                var category = new Category
                {
                    Code = categoryView.Code,
                    Name = categoryView.Name,
                    Price = categoryView.Price,
                    Active = categoryView.Active,
                    Status = categoryView.Status
                };

                var check = _CateRepo.CheckIsExists(category);

                if (await _CateRepo.Create(category, check) != null)
                {
                    return Json("1");
                }
                return Json("0");
            }
            return Json("0");
        }

        [HttpPost("getbyid")]
        public IActionResult GetById([FromBody]int idCate)
        {
            if (idCate > 0)
            {
                var category = _CateRepo.GetById(Convert.ToInt32(idCate)).Result;
                if (category != null)
                {
                    return Json(category);
                }
            }
            return Json("0");

        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]CategoryView categoryView)
        {
            var rs = _CateRepo.Update(categoryView.Id, new Category { Id = categoryView.Id, Name = categoryView.Name, Active = categoryView.Active, Status = categoryView.Status, Code = categoryView.Code, Price = categoryView.Price }).Result;
            if (rs)
            {
                return Json("1");
            }

            return Json("0");
        }


        [HttpPost("getidremove")]
        public async Task<IActionResult> GetIdRemove([FromBody]string[] arr)
        {
            return Json("0");
        }

    }
}