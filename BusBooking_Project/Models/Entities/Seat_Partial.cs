using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusBooking_Project.Models.Entities
{
    public class Seat_Partial
    {
        //[MaxLength(2)]
        //[MinLength(2)]
        //[Required]
        //[RegularExpression("(/^[A-Z0-9]{1,}$/)")]
        public string Code { get; set; }
    }

    [ModelMetadataType(typeof(Seat_Partial))]
    public partial class Seat : IEntity{ 
    
    }
}
