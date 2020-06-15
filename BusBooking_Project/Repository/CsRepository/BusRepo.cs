using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
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

        #region Get All BusView
        public List<BusView> GetAllBus()
        {
            return GetAll().Result.Where(p => p.Status == true).Select(p => new BusView
            {
                Id = p.Id,
                Code = p.Code,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                CateId = p.CateId,
                CateName = p.Cate.Name
            }).ToList();
        }
        #endregion

        #region Get BusView using Id
        public BusView GetBusById(int Id)
        {
            return GetAll().Result.Where(p => p.Status == true && p.Id == Id).Select(p => new BusView
            {
                Id = p.Id,
                Code = p.Code,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                CateId = p.CateId,
                CateName = p.Cate.Name
            }).FirstOrDefault();
        }
        #endregion

        public override bool CheckIsExists(Bus entity)
        {
            throw new NotImplementedException();
        }

        public List<BusView> GetBusByCateId(int CateId)
        {
            return GetAll().Result.Where(p => p.CateId == CateId).Select(p => new BusView
            {
                Id = p.Id,
                CateId = p.CateId,
                Code = p.Code,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                CateName = p.Cate.Name
            }).ToList();
        }
    }
}
