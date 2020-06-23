using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class SeatView 
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        [Required]
        [MinLength(2)]
        [RegularExpression("/^[A-Z0-9]{2,}$/")]
        public string Code { get; set; }
        public bool Status { get; set; }
        public string BusCode { get; set; }

        public bool Selected { get; set; }
        public DateTime DateSelected { get; set; }
    }
}
