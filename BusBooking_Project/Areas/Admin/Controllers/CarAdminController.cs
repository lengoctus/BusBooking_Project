using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Supports;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/car")]

    public class CarAdminController : Controller
    {
        private readonly IBusRePo _IBusrepo;    
        private readonly IWebHostEnvironment webHostEnvironment;

        public CarAdminController(IWebHostEnvironment webHostEnvironment, IBusRePo iBusrepo)
        {
            this.webHostEnvironment = webHostEnvironment;
            _IBusrepo = iBusrepo;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            ViewBag.ListBus = _IBusrepo.GetAllBus();
            return View("Index");
        }

        [HttpPost("add")]
        public IActionResult Add( BusView busView)
        {
            if (busView != null)
            {
                var bus = new Bus
                {
                    Id = busView.Id,
                    Code = busView.Code,
                    TotalSeat = busView.TotalSeat,
                    SeatEmpty = busView.SeatEmpty,
                    Active = busView.Active,
                    Status = busView.Status,
                    Image = busView.Image,
                    CateId = busView.CateId
                };

                if (_IBusrepo.CreateBus(bus) != null)
                {
                    return Json("1");
                }
            }
            return View("Add");
        }
   }
}