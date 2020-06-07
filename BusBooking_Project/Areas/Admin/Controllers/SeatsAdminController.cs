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
            ViewBag.listBus = _IBus.GetAll().Result.Where(p => p.Status == true).ToList();

            return View();
        }

        [HttpPost("GetByBusId")]
        public IActionResult GetByBusId([FromBody]string idbus)
        {
            var listSeat = _ISeat.GetByBusId(Convert.ToInt32(idbus)).Result.Select(p => new SeatView { 
                Id = p.Id,
                BusId = p.BusId,
                Code = p.Code
            }).ToList();

            if (listSeat.Count() > 0)
            {
             
                return Json(listSeat);
            }
            return Json("0");
        }
    }
}