using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Supports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class BusRepo : GenericRepo<Bus>, IBusRePo
    {
        private int size = ConstantACE.size;
        private string search = ConstantACE.search;
        public BusRepo(ConnectDbContext db) : base(db)
        {
        }

       
        public override bool CheckIsExists(Bus entity)
        {
            try
            {
                var bus = GetAll().Result.AsNoTracking().FirstOrDefault(p => p.Code.ToLower() == entity.Code.ToLower().Trim());
                if (bus!= null)
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

        public List<BusView> GetAllBus(int page)
        {
            int start = size * (page - 1);
            return GetAll().Result
                .Where(p => p.Status == true)
                .OrderByDescending(p => p.Id)
                .Skip(start)
                .Take(size)                
                .Select(p => new BusView
            {
                Id = p.Id,
                Code = p.Code,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                CategoryName = p.Category.Name
            }).ToList();

        }
        public int CountAllBus()
        {
            return GetDataACE()
                .Where(s => (bool)s.Status)
                .Count();
        }

        public int CreateACE(BusView busView)
        {
            int check = CheckCreate(busView);
            if (check == (int)CheckError.Success)
            {
                var bus = new Bus
                {
                    Code = busView.Code,
                    TotalSeat = busView.TotalSeat,
                    SeatEmpty = busView.SeatEmpty,
                    Image = busView.Image,
                    CateId = busView.CateId,
                    Status = true,
                    Active = true,
                };
                try
                {
                    Bus bus_1 = Add(bus).Result;
                    if (bus_1 == null) return (int)CheckError.ErrorOrther;
                }
                catch (Exception e)
                {
                    return (int)CheckError.ErrorOrther;
                }
                return (int)CheckError.Success;
            }
            else
            {
                return check;
            }
        }
        private int CheckCreate(BusView busView)
        {
            try
            {
                Bus busCode = GetDataACE().SingleOrDefault(s => s.Code.Trim() == busView.Code.Trim());
                if (busCode != null)
                {
                    return (int)CheckError.AlreadyCode;
                }
                return (int)CheckError.Success;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return (int)CheckError.ErrorOrther;
            }
        }
        private string GetCode()
        {
            Bus bus = GetDataACE().OrderByDescending(s => s.Id).FirstOrDefault();
            if (bus == null) return "00000";
            string code_Old = bus.Code;
            int b = Convert.ToInt32(code_Old);
            string code_New = b.ToString();
            while (b < 10000 && code_New.Length < 5)
            {
                code_New = "0" + code_New;
            }
            return code_New;
        }

        public BusView GetByIdBus(int id)
        {
            Bus bus = GetById(id).Result;
            return new BusView
            {
                Id = bus.Id,
                Code = bus.Code,           
                Image = bus.Image,           
                Active = bus.Active,
                Status = bus.Status,
                TotalSeat = bus.TotalSeat,
                SeatEmpty = bus.SeatEmpty,
                CateId = bus.CateId,
                CategoryName = bus.Category.Name
            };
        }
        public int UpdateBus(BusView busView)
        {
            int check = CheckModify(busView);
            if (check == (int)CheckError.Success)
            {
                Bus bus = GetById(busView.Id).Result;
                bus.Code = busView.Code;
                bus.Image = busView.Image;
                bus.CateId = busView.CateId;
                bus.TotalSeat = busView.TotalSeat;
                bus.SeatEmpty = busView.SeatEmpty;
                if (Update(bus.Id, bus).Result) return bus.Id;
                return (int)CheckError.ErrorOrther;
            }
            return check;
        }
        private int CheckModify(BusView busView)
        {
            Bus busCode = GetDataACE().SingleOrDefault(s => s.Id != busView.Id && s.Code.Trim() == busView.Code.Trim());
            if (busCode != null)
            {
                return (int)CheckError.AlreadyCode;
            }
            return (int)CheckError.Success;                       
        }

        public bool SetActive(int id)
        {
            Bus bus = GetById(id).Result;
            bus.Active = !bus.Active;
            return Update(bus.Id, bus).Result;
        }

        public List<BusView> Search(int page, string textsearch, int search_case)
        {
            int start = size * (page - 1);
            string columnSearch = "";
            switch (search_case)
            {
                
                case (int)SearchBus.Code:
                    columnSearch = "[code]";
                    break;
              
            }
            return GetDataRawSqlACE($"SELECT * FROM [bus] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1")                            
                .Select(s => new BusView
                {
                    Id = s.Id,
                    Code = s.Code,
                    TotalSeat = s.TotalSeat,
                    SeatEmpty = s.SeatEmpty,
                    Active = s.Active,
                    Status = s.Status,
                    Image = s.Image ,
                    CateId = s.CateId,
                    CategoryName = s.Category.Name                 
                }).ToList();
        }

        public int CountSearchData(string textsearch, int search_case)
        {
            string columnSearch = "";
            switch (search_case)
            {               
               
                case (int)SearchBus.Code:
                    columnSearch = "[code]";
                    break;
               
            }
            return GetDataRawSqlACE($"SELECT * FROM [bus] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1").Count();
        }

        public List<BusView> SearchByCategory(int page, int cateid)
        {
            int start = size * (page - 1);
            return GetAll().Result
                .Where(p => p.CateId == cateid && p.Status == true)
                .OrderByDescending(p => p.Id)
                .Skip(start)
                .Take(size)
                .Select(p => new BusView
                {
                    Id = p.Id,
                    Code = p.Code,
                    TotalSeat = p.TotalSeat,
                    SeatEmpty = p.SeatEmpty,
                    Active = p.Active,
                    Status = p.Status,
                    Image = p.Image,
                    CategoryName = p.Category.Name
                }).ToList();
        }
        public int CountSearchByCategory(int cateid)
        {
            return GetAll().Result.Where(p => p.CateId == cateid && p.Status == true).Select(p => new BusView
            {
                Id = p.Id,
                Code = p.Code,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                CategoryName = p.Category.Name
            }).Count();
        }
    }

}
