using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.SupportsTu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using BusBooking_Project.Models.ModelsView;
using System.Threading.Tasks;

namespace BusBooking_Project.ViewComponents
{
    [ViewComponent(Name = "SelectedSeatInfo")]
    public class SelectedSeatInfo : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View("index");
        }
    }
}
