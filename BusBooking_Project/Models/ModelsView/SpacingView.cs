using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class SpacingView
    {
        public int Id { get; set; }
        public int FromSp { get; set; }
        public string FromSpacingName { get; set; }
        public int ToSp { get; set; }
        public string ToSpacingName { get; set; }
        public decimal Price { get; set; }
        public int Length { get; set; }
        public TimeSpan? TimeGo { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }
        public int BusId { get; set; }
        public string BusName { get; set; }
        public TimeSpan? TimeRun { get; set; }
    }
}
