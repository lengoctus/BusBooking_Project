using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.ViewComponents
{
    [ViewComponent(Name = "BookingPartialForm")]
    public class BookingPartialForm : ViewComponent
    {       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("index");
        }
    }
}
