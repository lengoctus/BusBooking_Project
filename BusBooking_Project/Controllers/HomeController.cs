﻿using System;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepo _IAcc;

        public HomeController(ILogger<HomeController> logger, IAccountRepo iAcc)
        {
            _logger = logger;
            _IAcc = iAcc;
        }

        public IActionResult Index()
        {
            var acc_view = new AccountView
            {
                Email = "ngoctu",
                Phone = "ahi",

            };


            var acc = new Account
            {
                Id = 1,
                Email = acc_view.Email,
                Phone = acc_view.Phone
            };

            var res = _IAcc.GetById(acc.Id).Result;

            return View();
        }


    }
}
