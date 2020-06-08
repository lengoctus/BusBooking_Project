using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/seats")]
    public class SeatsAdminController : Controller
    {
        private readonly ISeatRePo _ISeat;
        private readonly IBusRePo _IBus;

        public SeatsAdminController(ISeatRePo iSeat, IBusRePo iBus)
        {
            _ISeat = iSeat;
            _IBus = iBus;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            
            return View();
        }
        
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Route("create")]
        /*public IActionResult Create()
        {
            return View();
        }*/

        [HttpGet]
        [Route("modify")]
        public IActionResult Modify()
        {
            return View();
        }
    }
}