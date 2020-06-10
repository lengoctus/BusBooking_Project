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
        private readonly IWebHostEnvironment webHostEnvironment;
        private ConnectDbContext db;

        public CarAdminController(IWebHostEnvironment webHostEnvironment, ConnectDbContext db)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.db = db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {

            ViewBag.cars = db.Bus.ToList();
            return View("Index");
        }

        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var cars = db.Bus.Find(id);
            return View("Edit", cars);

        }

        
        [HttpPost("edit")]
        public IActionResult Edit( Bus bus, IFormFile inputphoto) 
        {
            string FileNameSave = "dui.jpg";
            if (inputphoto != null)
            {
                FileNameSave = FileACE.SaveFile(webHostEnvironment, inputphoto, "admin/image");
            }
            bus.Image = FileNameSave;
            db.Entry(bus).State = 
                Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

            return View("Edit"); 
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {

            return View("Add", new Bus());
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Bus bus)
        {
            db.Bus.Add(bus);
            db.SaveChanges();


            return RedirectToAction("index", "bus");
        }
        [Route("success")]
        public IActionResult Success() 
        {
            return View("Success");
        }



        //[Route("delete/{id}")]
        //public IActionResult Delete(int id)
        //{
        //    db.Bus.Remove(db.Bus.Find(id));
        //    db.SaveChanges();
        //    return RedirectToAction("index", "bus");
        //}

    }
}