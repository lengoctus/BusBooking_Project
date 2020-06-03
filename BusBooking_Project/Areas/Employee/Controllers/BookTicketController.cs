using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BusBooking_Project.Areas.Employee.Controllers
{
    [Area("employee")]
    [Route("employee/bookticket")]
    public class BookTicketController : Controller  
    {
       
       
        [Route("index")]
        public IActionResult Index()
        {
            return View("Index");
        }
        
       
        [Route("login")]
        public IActionResult Login()    
        {
            return View("Login");
        }

      
        [Route("repw")]
        public IActionResult Repassword()   
        {
            return View("Repw");
        }
    }
}
