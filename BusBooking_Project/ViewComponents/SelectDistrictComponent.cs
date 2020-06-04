using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.ViewComponents
{
    [ViewComponent(Name = "SelectDistrictComponent")]
    public class SelectDistrictComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int cityID)
        {
            ViewBag.CityID = cityID;
            return View("index");
        }
    }
}
