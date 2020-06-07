using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class SeatRepo : GenericRepo<Seat>, ISeatRePo
    {
        public SeatRepo(ConnectDbContext db) : base(db)
        {
        }

        public Task<IQueryable<Seat>> GetByBusId(int idBus)
        {
            try
            {
                var listSeat = GetAll().Result.Where(p => p.BusId == idBus);
                if (listSeat.Count() > 0)
                {
                    return Task.FromResult(listSeat);
                }
                return Task.FromResult<IQueryable<Seat>>(null);
            }
            catch (Exception e)
            {
                var error = e.InnerException.Message;
                return Task.FromResult<IQueryable<Seat>>(null);
            }
        }

        public override bool CheckIsExists(Seat entity)
        {
            try
            {
                var seatCheck = GetAll().Result.FirstOrDefault(p => p.Code.ToLower() == entity.Code.ToLower().Trim());
                if (seatCheck != null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                var error = e.InnerException.Message;
                return false;
            }
        }
    }
}
