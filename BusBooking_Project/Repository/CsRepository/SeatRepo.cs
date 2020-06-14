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
        //private readonly ConnectDbContext _db;
        public SeatRepo(ConnectDbContext db) : base(db)
        {
            //_db = db;
        }

        public SeatView GetByIdSeat(int Id)
        {
            try
            {
                return GetAll().Result.Select(p => new SeatView
                {
                    Id = p.Id,
                    BusId = p.BusId,
                    Status = p.Status,
                    BusCode = p.Bus.Code,
                    Code = p.Code
                }).FirstOrDefault(p => p.Id == Id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<SeatView> SearchByBusId(int busid)
        {
            return GetAll().Result.Where(s => s.BusId == busid && s.Status == true).Select(s => new SeatView
            {
                Id = s.Id,
                BusId = s.BusId,
                Code = s.Code,
                BusCode = s.Bus.Code,
                Status = s.Status
            }).ToList();
        }

        public List<SeatView> GetAllSeat()
        {
            try
            {
                //.Join(db.Bus, se => se.BusId, bus => bus.Id, (se, bus) =>
                return GetAll().Result.Select(s => new SeatView
                {
                    Id = s.Id,
                    BusId = s.BusId,
                    Code = s.Code,
                    Status = s.Status,
                    BusCode = s.Bus.Code
                }).Where(p => p.Status == true).ToList();
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
                var seatCheck = GetAll().Result.FirstOrDefault(p => 
                    p.Code.ToLower() == entity.Code.ToLower().Trim() && p.BusId != entity.BusId);

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
        public bool Delete(int[] arrId)
        {
            for (int i = 0; i < arrId.Length; i++)
            {
                var seat = GetAll().Result.FirstOrDefault(p => p.Id == arrId[i] && p.Status == true);
                seat.Status = false;
                var rs = Update(arrId[i], seat).Result;
                if (!rs)
                {
                    return false;
                }

                if ((Array.IndexOf(arrId, arrId[i]) == arrId.Length - 1) && rs == true)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
