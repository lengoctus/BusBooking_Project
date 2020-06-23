using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class SeatRepo : GenericRepo<Seat>, ISeatRePo
    {
        private readonly ConnectDbContext _db;

        private int size2 = ConstantACE.size2;
        public SeatRepo(ConnectDbContext db) : base(db)
        {
            _db = db;
        }

        public SeatView GetByIdSeat(int Id)
        {
            try
            {
                return GetAll().Result.Where(s => s.Status == true).Select(p => new SeatView
                {
                    Id = p.Id,
                    BusId = p.BusId,
                    Status = p.Status ?? false,
                    BusCode = p.Bus.Code,
                    Code = p.Code
                }).FirstOrDefault(p => p.Id == Id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<SeatView> SearchByBus(int page, int busid)
        {
            int start = size2 * (page - 1);
            return GetAll().Result
                .Where(s => s.BusId == busid && s.Status == true)
                .OrderByDescending(p => p.Id)
                .Skip(start)
                .Take(size2)
                .Select(s => new SeatView
                {
                    Id = s.Id,
                    BusId = s.BusId,
                    Code = s.Code,
                    BusCode = s.Bus.Code,
                    Status = s.Status ?? false
                }).ToList();
        }
        public int CountSearchByBus(int busid)
        {
            return GetAll().Result
                .Where(s => s.BusId == busid && s.Status == true)
                .Select(s => new SeatView
                {
                    Id = s.Id,
                    BusId = s.BusId,
                    Code = s.Code,
                    BusCode = s.Bus.Code,
                    Status = s.Status ?? false
                }).Count();
        }

        public List<SeatView> GetAllSeat(int page)
        {
            int start = size2 * (page - 1);

            return GetAll().Result
                .Where(p => p.Status == true)
                .OrderByDescending(p => p.Id)
                .Skip(start)
                .Take(size2)
                .Select(s => new SeatView
                {
                    Id = s.Id,
                    BusId = s.BusId,
                    Code = s.Code,
                    Status = s.Status ?? false,
                    BusCode = s.Bus.Code
                }).ToList();
        }
        public int CountAllSeat()
        {
            return GetDataACE()
                .Where(s => (bool)s.Status)
                .Count();
        }

        public override bool CheckIsExists(Seat entity)
        {
            try
            {
                var seatCheck = GetAll().Result.AsNoTracking().FirstOrDefault(p => p.Code == entity.Code);

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

        public List<SeatView> GetAllByBusId(int busid)
        {
            try
            {
                return GetAll().Result.Where(p => p.Status == true && p.BusId == busid).Select(p => new SeatView
                {
                    Id = p.Id,
                    BusId = p.BusId,
                    Code = p.Code,
                    Status = p.Status ?? false
                }).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<int> GetSeatInBooking(int busId, DateTime dayStart)
        {
            try
            {
                var listSeat = GetAll().Result.Where(p => p.Status == true).Join(_db.Booking, sea => sea.Id, book => book.SeatId, (sea, book) => new SeatView
                {
                    Id = sea.Id,
                    BusId = sea.BusId,
                    DateSelected = book.DayStart
                }).Where(p => p.DateSelected == dayStart && p.BusId == busId).Select(p => p.Id).ToList();
                return listSeat;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

    }
}
