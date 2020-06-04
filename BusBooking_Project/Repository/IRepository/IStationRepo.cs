using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IStationRepo : IGenericRepo<Station>
    {
        bool CheckIsExists(Station entity);
        public Task<IQueryable<Category>> Search(int page, string textsearch, int case_search);
        public int CountDataSearch(string textsearch, int case_search);
        public List<Station> GetData(int page);
        public int CountData();
    }
}
