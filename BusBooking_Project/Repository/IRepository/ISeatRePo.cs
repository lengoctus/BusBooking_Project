﻿using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface ISeatRePo : IGenericRepo<Seat>
    {
        bool CheckIsExists(Seat entity);
        Task<IQueryable<Seat>> GetByBusId(int idBus);
    }
}