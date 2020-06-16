using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusBooking_Project.Models;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.CsRepository;
using BusBooking_Project.Models.Entities;

namespace BusBooking_Project.Controllers
{
    [Route("selectedSeat")]
    public class SelectedSeatController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     
        public SelectedSeatController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }


    }
}
