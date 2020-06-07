using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.ViewComponents
{
    [ViewComponent(Name = "MenuManager")]
    public class MenuManager : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index");
        }
    }
}
