using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.ViewComponents
{
    [ViewComponent(Name = "CustomerInfoForm")]
    public class CustomerInfoForm : ViewComponent
    {       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("index");
        }
    }
}
