using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.Entities
{
    public class Ticket_Partial
    {
    }

    [ModelMetadataType(typeof(Ticket_Partial))]
    public partial class Ticket :IEntity{
    
    }
}
