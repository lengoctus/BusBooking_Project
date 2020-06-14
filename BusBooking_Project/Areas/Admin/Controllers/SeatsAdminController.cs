using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Route("")]
        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.buses = _IBus.GetDataACE();
            ViewBag.seats = _ISeat.GetAllSeat();
            return View();
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            ViewBag.buses = _IBus.GetDataACE();
            return View("Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SeatView seatView)
        {
            if (seatView != null)
            {
                var seat = new Seat
                {
                    Code = seatView.Code,
                    BusId = seatView.BusId,
                    Status = seatView.Status
                };

                var check = _ISeat.CheckIsExists(seat);

                if (await _ISeat.Create(seat, check) != null)
                {
                    return Json("1");
                }

            }
            return Json("0");
        }

        [HttpPost("getbyid")]
        public IActionResult GetById([FromBody]int idSeat)
        {
            if (idSeat > 0)
            {
                var seat = _ISeat.GetByIdSeat(idSeat);
                if (seat != null)
                {
                    return Json(seat);
                }
            }
            return Json("0");
        }

        [HttpPost]
        [Route("modify")]
        public IActionResult Modify([FromBody]SeatView seatView)
        {
            var rs = _ISeat.Update(seatView.Id, new Seat
            {
                Id = seatView.Id,
                BusId = seatView.BusId,
                Code = seatView.Code,
                Status = seatView.Status
            }).Result;
            if (rs)
            {
                return Json("1");
            }
            return Json("0");
        }

        [HttpGet]
        [Route("searchbybusid/{id}")]
        public IActionResult SearchByBusId(int id)
        {
            return new JsonResult(_ISeat.SearchByBusId(id));
        }

        [HttpPost("deleteseat")]
        public IActionResult DeleteSeat([FromBody]string[] arrId)
        {
            int[] arrIdNew = Array.ConvertAll(arrId, p => Convert.ToInt32(p));
            var d = _ISeat.Delete(arrIdNew);
            if (d)
            {
                return Json("1");
            }
            return Json("0");
        }
    }
}