using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
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
        private readonly ConnectDbContext _db;
        public SeatRepo(ConnectDbContext db) : base(db)
        {
            _db = db;
        }



        public List<SeatView> Search(int busid)
        {
            return GetAll().Result.Where(s => s.BusId == busid).Select(s => new SeatView
            {
                Id = s.Id,
                BusId = s.BusId,
                Code = s.Code,
                Status = s.Status
            }).ToList();
        }

        public List<SeatView> GetAllSeat()
        {
            try
            {
                var listSeat = GetAll().Result.Join(_db.Bus, se => se.BusId, bus => bus.Id, (se, bus) => new SeatView
                {
                    Id = se.Id,
                    BusId = se.BusId,
                    Code = se.Code,
                    Status = se.Status,
                    BusCode = bus.Code
                }).ToList();
                return listSeat;
            }
            catch (Exception e)
            {
                return null;
             
            }
        }
        public override bool CheckIsExists(Seat entity)
        {
            try
            {
                var seatCheck = GetAll().Result.FirstOrDefault(p => p.Code.ToLower() == entity.Code.ToLower().Trim());

                if (seatCheck != null)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                var error = e.InnerException.Message;
                return false;
            }
        }
    }
}
