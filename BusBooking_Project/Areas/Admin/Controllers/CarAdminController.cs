using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BusBooking_Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/car")]
    public class CarAdminController : Controller
    {
        private ConnectDbContext db;
        public CarAdminController(ConnectDbContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            ViewBag.cars = db.Bus.ToList();
            return View("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)

        {
            var cars = db.Bus.Find(id);
            return View("EditCar", cars);

        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Bus bus)
        {
            db.Entry(bus).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditCar", "bus"); 
        }

    }
}