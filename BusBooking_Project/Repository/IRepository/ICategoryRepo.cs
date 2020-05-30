using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        public Task<IQueryable<Category>> Search(string keyword);
    }
}
