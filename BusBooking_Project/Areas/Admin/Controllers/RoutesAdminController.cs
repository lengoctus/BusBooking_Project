using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.SupportsTu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Authorize(Roles = "A", AuthenticationSchemes = "SCHEME_AD")]
    [Area("admin")]
    [Route("admin/routes")]
    public class RoutesAdminController : Controller
    {
        private readonly IRoutesRepo _IRout;
        private readonly IStationRepo _IStaion;
        private readonly ICategoryRepo _ICate;
        private readonly IBusRePo _IBus;

        public RoutesAdminController(IRoutesRepo iRout, IStationRepo iStaion, ICategoryRepo iCate, IBusRePo iBus)
        {
            _IRout = iRout;
            _IStaion = iStaion;
            _ICate = iCate;
            _IBus = iBus;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            ViewBag.Station = _IStaion.GetAllForRoutes();
            ViewBag.Category = _ICate.GetAllCategory();


            return View();
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] int fromid, [FromQuery] int toid, [FromQuery] int pgnb)
        {
            //string page = HttpContext.Request.Query["pgNumber"].ToString();
            int nbPage = pgnb == 0 ? 1 : Convert.ToInt32(pgnb);

            ViewBag.totalPage = _IRout.GetAllSearch(fromid, toid);        // Lấy tổng số dòng từ kết quả tìm kiếm
            ViewBag.pagesize = PaginationSupport.pagezise;      //  Số dòng mỗi trang
            var listRoutesBusView = _IRout.Search(fromid, toid, nbPage); // Filter data để phân trang 


            ViewBag.Station = _IStaion.GetAllForRoutes();
            ViewBag.Category = _ICate.GetAllCategory();
            ViewBag.fromid = fromid;
            ViewBag.toid = toid;
            ViewBag.currentPage = nbPage;
            return View("Index", listRoutesBusView);
        }

        [HttpPost("getbusbycateid")]
        public IActionResult GetBusByCateId([FromBody] string idCate)
        {
            if (!string.IsNullOrEmpty(idCate))
            {
                var listBus = _IBus.GetBusByCateId(Convert.ToInt32(idCate)).Select(p => new BusView
                {
                    Id = p.Id,
                    Code = p.Code
                }).ToList();
                if (listBus != null)
                {
                    return Json(listBus);
                }
            }
            return Json('0');
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] RoutesView routesView)
        {
            var rs = _IRout.AddRoutes(routesView);
            if (rs)
            {
                return Json("1");
            }
            return Json("0");
        }

        [HttpPost("getbyrouteid")]
        public IActionResult GetByRouteId([FromBody] int Id)
        {
            if (Id > 0)
            {
                var routeBus = _IRout.GetRoutesBusById(Id);
                routeBus.CategoryView.BusView = _IBus.GetBusByCateId(routeBus.CategoryView.Id).Where(p => p.Status == true).ToList();
                if (routeBus != null)
                {
                    return Json(routeBus);
                }
            }
            return Json("0");
        }

        [HttpPost("updateroutes")]
        public IActionResult UpdateRoutes([FromBody] RoutesView routesView)
        {
            routesView.Status = true;
            var rs = _IRout.UpdateRoute(routesView);
            if (rs)
            {
                return Json("1");
            }
            return Json("0");
        }
    }
}
