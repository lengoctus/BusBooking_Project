using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
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
        List<StationView> Search(int page, string textsearch, int case_search);
        int CountDataSearch(string textsearch, int case_search);
        List<StationView> GetData(int page);
        int CountData();
        StationView GetByIdACE(int id);
        int CreateACE(StationView stationView);
        int ModifyACE(StationView stationView);
        bool SetStatus(int id);
        bool SetActive(int id);
        bool RemoveACE(int stationId);
        List<StationView> GetAllTu();
    }
}
