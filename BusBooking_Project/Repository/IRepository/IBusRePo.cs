using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IBusRePo : IGenericRepo<Bus>
    {
        bool CheckIsExists(Bus entity);
        List<BusView> GetAllBus();
        int CreateACE(BusView busView);
        public int UpdateBus(BusView busView);
        public BusView GetByIdBus(int id);
        bool SetActive(int id);
        List<BusView> Search(string textsearch, int search_case);
        int CountSearchData(string textsearch, int search_case);
        List<BusView> GetBusByCateId(int CateId);
    }
}
