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
            ViewBag.listCate = _CateRepo.GetAllCategory();
            return View();
        }

        
        [HttpPost("add")]
        public  IActionResult Add([FromBody]CategoryView categoryView)
        {
            if (categoryView != null)
            {
                var category = new Category
                {
                    Code = categoryView.Code,
                    Name = categoryView.Name,
                    Price = categoryView.Price,
                    Active = categoryView.Active,
                    Status = true
                };

                if ( _CateRepo.CreateCate(category) != null)
                {
                    return Json("1");
                }
            }
            return Json("0");
        }

        [HttpPost("getbyid")]
        public IActionResult GetById([FromBody]int idCate)
        {
            if (idCate > 0)
            {
                var category = _CateRepo.GetByidCate(Convert.ToInt32(idCate));
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
            categoryView.Status = true;
            var rs = _CateRepo.UpdateCategory(categoryView.Id, categoryView);
            if (rs)
            {
                return Json("1");
            }

            return Json("0");
        }


        [HttpPost("upatestatus")]
        public IActionResult UpdateStatus([FromBody]int[] arr)
        {

            var rs = _CateRepo.UpdateStatus(arr);
            if (rs)
            {
                return Json("1");
            }

            return Json("0");
        }

    }
}