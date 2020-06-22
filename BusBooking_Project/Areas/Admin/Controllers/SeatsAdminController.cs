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
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            ViewBag.buses = _IBus.GetDataACE().Where(p => p.Status == true).ToList();
            var seats = _ISeat.GetAllSeat(page);
            ViewBag.Rows = _ISeat.CountAllSeat();
            return View(seats);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            var listBus = _IBus.GetDataACE().Where(p => p.Status == true).ToList().Where(p => p.Status == true).ToList();
            if (listBus != null && listBus.Count > 0)
            {
                ViewBag.buses = listBus;
            }
            return View("Create");
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SeatView seatView)
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
        public IActionResult GetById([FromBody] int idSeat)
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
        public IActionResult Modify([FromBody] SeatView seatView)
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
        [Route("searchbybus")]
        public IActionResult SearchByBus()
        {
            int id = Convert.ToInt32(HttpContext.Request.Query["id"].ToString().Trim());
            string strPage = HttpContext.Request.Query["page"].ToString();
            int page = Convert.ToInt32(strPage == "" ? "1" : strPage);
            var seatsbybus = _ISeat.SearchByBus(page, id);
            ViewBag.Rows = _ISeat.CountSearchByBus(id);
            ViewBag.buses = _IBus.GetDataACE().Where(p => p.Status == true).ToList();
            return View("index", seatsbybus);
        }

        [HttpPost("deleteseat")]
        public IActionResult DeleteSeat([FromBody] string[] arrId)
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