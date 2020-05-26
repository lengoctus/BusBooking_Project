using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.Entities
{
    public class Account_Partial
    {
    }

    [ModelMetadataType(typeof(Account_Partial))]
    public partial class Account : IEntity { }
}
