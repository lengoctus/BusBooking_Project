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
        //private int size = ConstantACE.size;
        private string search = ConstantACE.search;
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
                Name = p.Name,
                TotalSeat = p.TotalSeat,
                SeatEmpty = p.SeatEmpty,
                Active = p.Active,
                Status = p.Status,
                Image = p.Image,
                CategoryName = p.Category.Name
            }).ToList();

        }

        public int CreateACE(BusView busView)
        {
            int check = CheckCreate(busView);
            if (check == (int)CheckError.Success)
            {
                var bus = new Bus
                {
                    Name = busView.Name,
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
                Bus busName = GetDataACE().SingleOrDefault(s => s.Name.Trim().ToLower() == busView.Name.Trim().ToLower());
                if (busName != null)
                {
                    return (int)CheckError.AlreadyName;
                }
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
                Name = bus.Name,
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
                bus.Name = busView.Name;
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
            
            Bus busName = GetDataACE().SingleOrDefault(s => s.Id != busView.Id && s.Name.Trim().ToLower() == busView.Name.Trim().ToLower());
            if (busName != null)
            {
                return (int)CheckError.AlreadyName;
            }
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

        public List<BusView> Search(string textsearch, int search_case)
        {         
            string columnSearch = "";
            switch (search_case)
            {
                case (int)SearchBus.Name:
                    columnSearch = "[name]";
                    break;
                case (int)SearchBus.Code:
                    columnSearch = "[code]";
                    break;
              
            }
            return GetDataRawSqlACE($"SELECT * FROM [bus] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1")                             
                .Select(s => new BusView
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
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
                case (int)SearchBus.Name:
                    columnSearch = "[name]";
                    break;
               
                case (int)SearchBus.Code:
                    columnSearch = "[code]";
                    break;
               
            }
            return GetDataRawSqlACE($"SELECT * FROM [bus] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1").Count();
        }

    }

}
