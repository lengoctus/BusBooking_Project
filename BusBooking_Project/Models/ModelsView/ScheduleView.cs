using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class ScheduleView
    {
        public int Id { get; set; }
        public int StationFrom { get; set; }
        public int StationTo { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public TimeSpan TimeGo { get; set; }
        public TimeSpan TimeRun { get; set; }   
    }
}
