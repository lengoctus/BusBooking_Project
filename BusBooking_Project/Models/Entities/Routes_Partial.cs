using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.Entities
{
    public class Routes_Partial 
    {
    }

    [ModelMetadataType(typeof(Routes_Partial))]
    public partial class Routes : IEntity { }
}
