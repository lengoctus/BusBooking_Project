using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(ConnectDbContext db) : base(db)
        {
        }

        public override bool CheckIsExists(Category entity)
        {
            try
            {
                var category = GetAll().Result.FirstOrDefault(p => p.Code.ToLower() == entity.Code.ToLower().Trim());
                if (category != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public Task<IQueryable<Category>> Search(string keyword)
        {
            try
            {
                return Task.FromResult<IQueryable<Category>>(null);
            }
            catch (Exception e)
            {
                var error = e.Message;
                return Task.FromResult<IQueryable<Category>>(null);
            }
        }
    }
}
