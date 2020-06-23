using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BusBooking_Project.Areas.Employee.Controllers
{   // login thành công
    [Authorize(Roles = "E", AuthenticationSchemes = "SCHEME_EMP")]
    [Area("employee")]
    [Route("employee/bookticket")]
    public class BookTicketController : Controller  
    {
        private readonly ILogger<BookTicketController> _logger;
        private readonly IAccountRepo accountRepository;
        private IConfiguration configuration;

        public BookTicketController(
            ILogger<BookTicketController> logger,
            IAccountRepo _accountRepository,
            IConfiguration _configuration)
        {
            _logger = logger;
            accountRepository = _accountRepository;
            configuration = _configuration;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }   
        
        [HttpGet("bookticket")]
        public IActionResult Bookticket()
        {
            return View();
        }

        [HttpGet("sendmail")]
        public IActionResult Sendmail()
        {
            return View();
        }

    }
    //login thành công
}
