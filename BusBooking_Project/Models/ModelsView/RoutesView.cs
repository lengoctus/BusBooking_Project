﻿using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class RoutesView : IEntity
    {
        public int Id { get; set; }
        public int StationFrom { get; set; }
        public int StationTo { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public string TimeGo { get; set; }
        public bool? Active { get; set; }
        public bool? Status { get; set; }
        public int BusId { get; set; }
        public string TimeRun { get; set; }

        public string StationLocationFrom { get; set; }
        public string StationLocationTo { get; set; }
        public int CategoryId { get; set; }
    }
}
