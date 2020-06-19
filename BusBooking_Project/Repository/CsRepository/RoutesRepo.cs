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
    public class RoutesRepo : GenericRepo<Routes>, IRoutesRepo
    {
        public RoutesRepo(ConnectDbContext db) : base(db)
        {
        }
        #region Get All Routes
        public List<RoutesView> GetAllRoutes()
        {
            var routes = GetAll().Result.Where(p => p.Status == true).Select(p => new RoutesView
            {
                Id = p.Id,
                StationFrom = p.StationFrom,
                StationTo = p.StationTo,
                Price = p.Price,
                Length = p.Length,
                TimeGo = p.TimeGo,
                Active = p.Active,
                Status = p.Status,
                BusId = p.BusId,
                TimeRun = p.TimeRun
            }).ToList();
            return routes;
        }
        #endregion



        public override bool CheckIsExists(Routes entity)
        {
            throw new NotImplementedException();
        }
    }
}
