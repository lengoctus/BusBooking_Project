using BusBooking_Project.Repository.EFCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BusBooking_Project.Models.Entities
{
    public partial class Role_Partial
    {

    }

    [ModelMetadataType(typeof(Role_Partial))]
    public partial class Role : IEntity { }
}
