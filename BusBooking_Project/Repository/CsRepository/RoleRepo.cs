using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Supports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//e.InnerException.Message
//passhash = c44755c3379313db173e53c3e8fb6701

namespace BusBooking_Project.Repository.CsRepository
{
    public class RoleRepo : GenericRepo<Role>, IRoleRepo
    {
        #region ctor
        public override bool CheckIsExists(Role entity)
        {
            throw new NotImplementedException();
        }
        public RoleRepo(ConnectDbContext db) : base(db)
        {

        }
        #endregion

        public Role GetByIdACE(int id)
        {
            return GetByIdACE(id);
        }

        List<Role> IRoleRepo.GetDataACE()
        {
            return GetDataACE().Where(s => s.Name != "A").Select(s => new Role
            {
                Id = s.Id,
                Name = s.Name == "A" ? "Admin" : (s.Name == "E" ? "Employee" : "Customer")
            }).ToList();
        }
    }
}
