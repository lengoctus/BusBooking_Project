using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class BusRepo : GenericRepo<Bus>, IBusRePo
    {
        public BusRepo(ConnectDbContext db) : base(db)
        {
        }


        public override bool CheckIsExists(Bus entity)
        {
            throw new NotImplementedException();
        }
    }
}
