using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using BusBooking_Project.SupportsTu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class RoutesRepo : GenericRepo<Routes>, IRoutesRepo
    {
        private ConnectDbContext _db;
        public RoutesRepo(ConnectDbContext db) : base(db)
        {
            _db = db;
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
                TimeGo = p.TimeGo.ToString(),
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
            var check = GetAll().Result.FirstOrDefault(p => p.StationFrom == entity.StationFrom && p.StationTo == entity.StationTo && p.BusId == entity.BusId);
            if (check != null)
            {
                return true;
            }
            return false;
        }

        #region Search Routes
        public List<RoutesBusView> Search(int fromid, int toid, int nbPage)
        {
            try
            {
                var listRoute = GetAll().Result.Where(p => p.Status == true && p.StationFrom == fromid && p.StationTo == toid);

                var listRoutesSearch = _db.Bus.Join(listRoute, bus => bus.Id, rou => rou.BusId, (bus, rou) => new RoutesBusView
                {
                    BusView = new BusView
                    {
                        Id = bus.Id,
                        Code = bus.Code
                    },
                    RoutesView = new RoutesView
                    {
                        Id = rou.Id,
                        StationFrom = rou.StationFrom,
                        StationTo = rou.StationTo,
                        Price = rou.Price,
                        Length = rou.Length,
                        TimeGo = rou.TimeGo.ToString(),
                        TimeRun = rou.TimeRun,
                        BusId = rou.BusId
                    },
                    CategoryView = new CategoryView
                    {
                        Id = bus.Cate.Id,
                        Name = bus.Cate.Name
                    }
                }).Skip(PaginationSupport.GetRows(nbPage)).Take(PaginationSupport.pagezise).ToList();


                return listRoutesSearch;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return null;
            }
        }
        #endregion

        #region Count Data To Search
        public int GetAllSearch(int fromid, int toid)
        {
            try
            {
                var listRoute = GetAll().Result.Where(p => p.Status == true && p.StationFrom == fromid && p.StationTo == toid).Count();

                return listRoute;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return 0;
            }
        }
        #endregion

        #region Create new Routes
        public bool AddRoutes(RoutesView routesView)
        {
            if (routesView != null)
            {
                var routes = new Routes
                {
                    StationFrom = routesView.StationFrom,
                    StationTo = routesView.StationTo,
                    Price = routesView.Price,
                    Length = routesView.Length,
                    TimeGo = TimeSpan.Parse(routesView.TimeGo),
                    Active = routesView.Active,
                    Status = routesView.Status,
                    BusId = routesView.BusId,
                    TimeRun = routesView.TimeRun
                };

                var routesReturn = Create(routes, CheckIsExists(routes)).Result;
                if (routesReturn != null)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
