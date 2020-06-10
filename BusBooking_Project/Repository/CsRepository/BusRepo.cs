using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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


        

        public override bool CheckIsExists(Bus bus)
        {
            try
            {
                var cate = GetAll().Result.AsNoTracking().FirstOrDefault(p => p.Code.ToLower() == bus.Code.ToLower().Trim());
                if (cate != null)
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

        public List<BusView> GetAllBus()
        {
            return GetAll().Result.Where(p => p.Status == true).Select(p => new BusView
            {
                Id = p.Id,
                Code = p.Code,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                Active = p.Active ?? false,
                Status = p.Status ?? false,
                Image = p.Image,
                CateId = p.CateId
            }).ToList();

        }

        public BusView CreateBus(Bus bus)
        {
            var result = Create(bus, CheckIsExists(bus)).Result;
            if (result != null)
            {
                return new BusView
                {
                    Id = result.Id,
                    Code = result.Code,
                    TotalSeat = result.TotalSeat,
                    SeatEmpty = result.SeatEmpty,
                    Image = result.Image,
                    CateId = result.CateId,
                    Active = result.Active ?? true,
                    Status = result.Status ?? true
                };
            }
            return null;
        }

    }
}
