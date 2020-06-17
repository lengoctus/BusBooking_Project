using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IRoutesRepo : IGenericRepo<Routes>
    {
        bool AddRoutes(RoutesView routesView);
        List<RoutesView> GetAllRoutes();
        int GetAllSearch(int fromid, int toid);
        RoutesBusView GetRoutesBusById(int Id);
        List<RoutesView> GetRoutesFrom();
        List<RoutesView> GetRoutesTo(int idFrom);
        List<RoutesBusView> Search(int fromid, int toid, int nbPage);
    }
}
