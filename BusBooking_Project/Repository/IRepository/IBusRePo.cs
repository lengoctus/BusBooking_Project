using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IBusRePo : IGenericRepo<Bus>
    {
        bool CheckIsExists(Bus entity);
    }
}
