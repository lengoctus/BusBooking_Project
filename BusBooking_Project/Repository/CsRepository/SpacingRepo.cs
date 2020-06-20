using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class SpacingRepo : GenericRepo<Spacing>, ISpacingRepo
    {
        #region ctor
        public override bool CheckIsExists(Spacing entity)
        {
            throw new NotImplementedException();
        }
        private string search = ConstantACE.search;
        private int size = ConstantACE.size;
        //int start = size * (page - 1);
        public SpacingRepo(ConnectDbContext db) : base(db) { }
        #endregion

        #region Create
        public int CreateACE(SpacingView spacingView)
        {
            int check = CheckCreate(spacingView);
            if (check == (int)CheckError.Success)
            {
                Spacing spacing = new Spacing
                {
                    Active = spacingView.Active,
                    Status = true,
                    FromSp = spacingView.FromSp,
                    TimeGo = spacingView.TimeGo,
                    Price = spacingView.Price,
                    Length = spacingView.Length,
                    TimeRun = spacingView.TimeRun,
                    ToSp = spacingView.ToSp,
                };

                Spacing spacing_1 = Add(spacing).Result;
                if (spacing_1 == null) return (int)CheckError.ErrorOrther;
                return (int)CheckError.Success;
            }
            return check;
        }

        private int CheckCreate(SpacingView spacingView)
        {
            return (int)CheckError.Success;
        }
        #endregion

        #region Modify
        public int ModifyACE(SpacingView spacingView)
        {
            int check = CheckModify(spacingView);
            if (check == (int)CheckError.Success)
            {
                Spacing spacing = GetById(spacingView.Id).Result;
                spacing.FromSp = spacingView.FromSp;
                spacing.Length = spacingView.Length;
                spacing.Price = spacingView.Price;
                spacing.TimeGo = spacingView.TimeGo;
                spacing.TimeRun = spacingView.TimeRun;
                spacing.ToSp = spacingView.ToSp;
                spacing.BusId = spacingView.BusId;
                if (Update(spacing.Id, spacing).Result) return spacing.Id;
                return (int)CheckError.ErrorOrther;
            }
            return check;
        }

        private int CheckModify(SpacingView spacingView)
        {
            return (int)CheckError.Success;
        }

        public bool SetActive(int id)
        {
            Spacing spacing = GetById(id).Result;
            spacing.Active = !spacing.Active;
            return Update(spacing.Id, spacing).Result;
        }

        public bool SetStatus(int id)
        {
            Spacing spacing = GetById(id).Result;
            spacing.Status = !spacing.Status;
            return Update(spacing.Id, spacing).Result;
        }

        #endregion

        #region Remove
        public bool RemoveACE(int id)
        {
            return Delete(id).Result;
        }
        #endregion

        #region GetDataPaging
        public List<SpacingView> GetData(int page)
        {
            int start = size * (page - 1);
            return GetDataACE()
                .Where(s => s.Status == true)
                .OrderByDescending(s => s.Id)
                .Skip(start)
                .Take(size)
                .Select(s => new SpacingView
                {
                    Id = s.Id,
                    Active = (bool)s.Active,
                    Status = (bool)s.Status,
                    BusId = s.BusId,
                    FromSp = s.FromSp,
                    Length = s.Length,
                    Price = s.Price,
                    TimeGo = s.TimeGo,
                    TimeRun = s.TimeRun,
                    ToSp = s.ToSp,
                }).ToList();
        }

        public int CountData()
        {
            return GetDataACE()
                .Where(s => (bool)s.Status)
                .Count();
        }

        public SpacingView GetByIdACE(int id)
        {
            Spacing spacing = GetById(id).Result;
            if (spacing != null)
            {
                return new SpacingView
                {
                    Id = spacing.Id,
                    Active = (bool)spacing.Active,
                    Status = (bool)spacing.Status,
                    FromSp=spacing.FromSp,
                    BusId=spacing.BusId,
                    Length=spacing.Length,
                    Price=spacing.Price,
                    TimeGo=spacing.TimeGo,
                    TimeRun=spacing.TimeRun,
                    ToSp=spacing.ToSp
                };
            }
            return null;
        }
        #endregion

        #region SearchData
        public List<SpacingView> SearchData(int page, string textsearch, int case_search)
        {
            int start = size * (page - 1);
            string columnSearch = "";
            switch (case_search)
            {
                case (int)SearchSpacing.FromName:
                    columnSearch = "[fromsp]";
                    break;
                case (int)SearchSpacing.ToName:
                    columnSearch = "[tosp]";
                    break;
                case (int)SearchSpacing.TimeGo:
                    columnSearch = "[timego]";
                    break;
                case (int)SearchSpacing.TimeRun:
                    columnSearch = "[timerun]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [spacing] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1")
                .Skip(start)
                .Take(size)
                .Select(s => new SpacingView
                {
                    Id = s.Id,
                    Active = (bool)s.Active,
                    Status = (bool)s.Status,
                    FromSp = s.FromSp,
                    ToSp = s.ToSp,
                    TimeGo = s.TimeGo,
                    TimeRun = s.TimeRun,
                    BusId = s.BusId,
                    Length = s.Length,
                    Price = s.Price,
                }).ToList();
        }

        public int CountSearchData(string textsearch, int case_search)
        {
            string columnSearch = "";
            switch (case_search)
            {
                case (int)SearchSpacing.FromName:
                    columnSearch = "[FromSp]";
                    break;
                case (int)SearchSpacing.ToName:
                    columnSearch = "[ToSp]";
                    break;
                case (int)SearchSpacing.TimeGo:
                    columnSearch = "[timego]";
                    break;
                case (int)SearchSpacing.TimeRun:
                    columnSearch = "[timerun]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [spacing] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1").Count();
        }


        #endregion
    }
}
