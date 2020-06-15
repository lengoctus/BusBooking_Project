using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Supports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBooking_Project.Repository.CsRepository
{
    public class StationRepo : GenericRepo<Station>, IStationRepo
    {
        #region ctor
        public override bool CheckIsExists(Station entity)
        {
            throw new NotImplementedException();
        }
        private string search = ConstantACE.search;
        private int size = ConstantACE.size;
        //int start = size * (page - 1);
        public StationRepo(ConnectDbContext db) : base(db)
        {

        }
        #endregion

        #region GetData Paging
        public int CountData()
        {
            return GetDataACE()
                .Where(s => s.Status)
                .Count();
        }
        public List<StationView> GetData(int page)
        {
            int start = size * (page - 1);
            return GetDataACE()
                .Where(s => s.Status)
                .OrderByDescending(s => s.Id)
                .Skip(start)
                .Take(size)
                .Select(s => new StationView
                {
                    Id = s.Id,
                    Active = s.Active,
                    Location = s.Location,
                    City = (int)s.City,
                    Phone = s.Phone,
                    Name = s.Name,
                    Status = s.Status,
                    District = (int)s.District
                }).ToList();
        }
        public StationView GetByIdACE(int id)
        {
            Station station = GetById(id).Result;
            if (station != null)
            {
                return new StationView
                {
                    Id = station.Id,
                    Active = station.Active,
                    City = (int)station.City,
                    District = (int)station.District,
                    Location = station.Location,
                    Name = station.Name,
                    Phone = station.Phone,
                    Status = station.Status
                };
            }
            return null;
        }
        #endregion

        #region Search Paging
        public int CountDataSearch(string textsearch, int case_search)
        {
            string columnSearch = "";
            switch (case_search)
            {
                case (int)SearchStation.Name:
                    columnSearch = "[name]";
                    break;
                case (int)SearchStation.Phone:
                    columnSearch = "[phone]";
                    break;
                case (int)SearchStation.Location:
                    columnSearch = "[location]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [station] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1").Count();
        }
        public List<StationView> Search(int page, string textsearch, int case_search)
        {
            int start = size * (page - 1);
            string columnSearch = "";
            switch (case_search)
            {
                case (int)SearchStation.Name:
                    columnSearch = "[name]";
                    break;
                case (int)SearchStation.Phone:
                    columnSearch = "[phone]";
                    break;
                case (int)SearchStation.Location:
                    columnSearch = "[location]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [station] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1")
                .Skip(start)
                .Take(size)
                .Select(s => new StationView
                {
                    Id = s.Id,
                    Active = s.Active,
                    Name = s.Name,
                    City = (int)s.City,
                    District = (int)s.District,
                    Location = s.Location,
                    Phone = s.Phone,
                    Status = s.Status
                }).ToList();
        }
        #endregion

        #region Create
        public int CreateACE(StationView stationView)
        {
            int check = CheckCreate(stationView);
            if (check == (int)CheckError.Success)
            {
                Station station = new Station
                {
                    City = stationView.City,
                    District = stationView.District,
                    Active = stationView.Active,
                    Location = stationView.Location,
                    Name = stationView.Name,
                    Phone = stationView.Phone,
                    Status = true
                };
                try
                {
                    Station station_1 = Add(station).Result;
                    if (station_1 == null) return (int)CheckError.ErrorOrther;
                }
                catch (Exception e)
                {
                    return (int)CheckError.ErrorOrther;
                }
                return (int)CheckError.Success;
            }
            return check;
        }
        private int CheckCreate(StationView stationView)
        {
            Station stationName = GetDataACE()
                .SingleOrDefault(s => s.Name.ToLower() == stationView.Name.ToLower().Trim());
            if (stationName != null) return (int)CheckError.AlreadyName;
            Station stationPhone = GetDataACE()
            .SingleOrDefault(s => s.Phone == stationView.Phone.Trim());
            if (stationPhone != null) return (int)CheckError.AlreadyPhone;
            Station stationCity = GetDataACE().FirstOrDefault(s => s.City == stationView.City);
            if (stationCity != null) return (int)CheckError.AlreadyLocationCity;
            return (int)CheckError.Success;
        }


        #endregion

        #region Modify
        public int ModifyACE(StationView stationView)
        {
            int check = CheckModify(stationView);
            if (check == (int)CheckError.Success)
            {
                Station station = GetById(stationView.Id).Result;
                station.Name = stationView.Name;
                station.Phone = stationView.Phone;
                station.Location = stationView.Location;
                station.City = stationView.City;
                station.District = stationView.District;
                if (Update(station.Id, station).Result) return station.Id;
                return (int)CheckError.ErrorOrther;
            }
            return check;
        }

        private int CheckModify(StationView stationView)
        {
            Station stationName = GetDataACE()
                .SingleOrDefault(s => s.Id != stationView.Id && s.Name.ToLower() == stationView.Name.ToLower().Trim());
            if (stationName != null)
            {
                return (int)CheckError.AlreadyName;
            }
            Station stationPhone = GetDataACE()
            .SingleOrDefault(s => s.Id != stationView.Id && s.Phone == stationView.Phone.Trim());
            if (stationPhone != null)
            {
                return (int)CheckError.AlreadyPhone;
            }
            return (int)CheckError.Success;
        }

        public bool SetStatus(int id)
        {
            Station station = GetById(id).Result;
            station.Status = !station.Status;
            return Update(station.Id, station).Result;
        }

        public bool SetActive(int id)
        {
            Station station = GetById(id).Result;
            station.Active = !station.Active;
            return Update(station.Id, station).Result;
        }

        #endregion

        #region Remove
        public bool RemoveACE(int stationId)
        {
            try
            {
                Delete(stationId);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
