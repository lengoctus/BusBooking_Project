using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Supports;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/home")]
    public class HomeAdminController : Controller
    {
        

        public HomeAdminController()
        {
           
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
