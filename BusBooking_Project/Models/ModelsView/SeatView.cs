using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class SeatView : IEntity
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public string BusCode { get; set; }
    }
}
