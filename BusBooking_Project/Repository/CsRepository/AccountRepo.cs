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
//e.InnerException.Message
//passhash = c44755c3379313db173e53c3e8fb6701

namespace BusBooking_Project.Repository.CsRepository
{
    public class AccountRepo : GenericRepo<Account>, IAccountRepo
    {
        #region ctor
        public override bool CheckIsExists(Account account)
        {
            return false;
        }

        private string search = ConstantACE.search;
        private int size = ConstantACE.size;
        //int start = size * (page - 1);
        public AccountRepo(ConnectDbContext db) : base(db)
        {

        }
        #endregion

        #region Create
        public int CreateACE(AccountView accountView)
        {
            int check = CheckCreate(accountView);
            if (check == (int)CheckError.Success)
            {
                Account account = new Account
                {
                    Address = accountView.Address,
                    Code = GetCode(),
                    DayCreate = DateTime.Now,
                    DayEdited = DateTime.Now,
                    Description = accountView.Description,
                    Dob = accountView.Dob,
                    Email = accountView.Email,
                    Gender = (byte)accountView.Gender,
                    Images = accountView.Images,
                    Name = accountView.Name,
                    Password = MD5ACE.Encrypt(accountView.Password),
                    Phone = accountView.Phone,
                    RoleId = (byte)accountView.RoleId,
                    StationId = accountView.StationId,
                    Status = true,
                    Active = true,
                };
                try
                {
                    Add(account);
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
        private int CheckCreate(AccountView accountView)
        {
            try
            {
                Account accountEmail = GetDataACE().SingleOrDefault(s => s.Email.Trim().ToLower() == accountView.Email.Trim().ToLower());
                if (accountEmail != null)
                {
                    return (int)CheckError.AlreadyEmail;
                }
                Account accountPhone = GetDataACE().SingleOrDefault(s => s.Phone.Trim() == accountView.Phone.Trim());
                if (accountPhone != null)
                {
                    return (int)CheckError.AlreadyPhone;
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
            Account acc = GetDataACE().OrderByDescending(s => s.Id).FirstOrDefault();
            if (acc == null) return "000000";
            string code_Old = acc.Code;
            int b = Convert.ToInt32(code_Old);
            string code_New = b.ToString();
            while (b < 100000 && code_New.Length < 6)
            {
                code_New = "0" + code_New;
            }
            return code_New;
        }

        #endregion

        #region GetData paging
        public List<AccountView> GetData(int page)
        {
            int start = size * (page - 1);
            return GetDataACE()
                .Where(s => s.Status == true)
                .OrderByDescending(s => s.Id)
                .Skip(start)
                .Take(size)
                .Select(s => new AccountView
                {
                    Id = s.Id,
                    Active = (bool)s.Active,
                    RoleName = s.Role.Name,
                    Name = s.Name,
                    Address = s.Address,
                    Code = s.Code,
                    DayCreate = (DateTime)s.DayCreate,
                    DayEdited = (DateTime)s.DayEdited,
                    Description = s.Description,
                    Dob = (DateTime)s.Dob,
                    EditerId = (int)s.EditerId,
                    Email = s.Email,
                    Gender = (int)s.Gender,
                    Images = s.Images,
                    Password = s.Password,
                    Phone = s.Phone,
                    RoleId = s.Role.Id,
                    StationId = (int)s.StationId,
                    StationName = s.Station.Name
                }).ToList();
        }
        public int CountData()
        {
            return GetDataACE()
                .Where(s => (bool)s.Status)
                .Count();
        }
        public AccountView GetByIdACE(int id)
        {
            Account account = GetById(id).Result;
            return new AccountView
            {
                Id = account.Id,
                Address = account.Address,
                Code = account.Code,
                DayCreate = (DateTime)account.DayCreate,
                DayEdited = (DateTime)account.DayEdited,
                Description = account.Description,
                Dob = (DateTime)account.Dob,
                EditerId = account.EditerId ?? 0,
                Editer = account.EditerId != null ? GetById((int)account.EditerId).Result : null,
                Email = account.Email,
                Gender = (int)account.Gender,
                Name = account.Name,
                Images = account.Images,
                Password = account.Password,
                Phone = account.Phone,
                Active = (bool)account.Active,
                Status = (bool)account.Status,
                StationId = (int)account.StationId,
            };
        }

        #endregion

        #region Login
        public AccountView Login(Account account)
        {
            string passHash = MD5ACE.Encrypt(account.Password);
            return GetAll().Result
                .Where(s => s.Email.ToLower().Trim() == account.Email.ToLower().Trim() && s.Password == passHash)
                .Select(s => new AccountView
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Address = s.Address,
                    DayCreate = (DateTime)s.DayCreate,
                    DayEdited = (DateTime)s.DayEdited,
                    Description = s.Description,
                    Active = (bool)s.Active,
                    Dob = (DateTime)s.Dob,
                    Email = s.Email,
                    ForgotPass = s.ForgotPass,
                    Gender = (int)s.Gender,
                    Images = s.Images,
                    Password = s.Password,
                    Phone = s.Phone,
                    RoleName = s.Role.Name,
                    Status = (bool)s.Status,
                    EditerId = (int)s.EditerId
                }).SingleOrDefault();
        }

        #endregion

        #region Modify
        public int ModifyACE(AccountView accountView, int EditerId)
        {
            int check = CheckModify(accountView);
            if (check == (int)CheckError.Success)
            {
                Account account = GetById(accountView.Id).Result;
                account.Address = accountView.Address;
                account.DayEdited = DateTime.Now;
                account.EditerId = EditerId;
                account.Email = accountView.Email;
                account.Gender = (byte)accountView.Gender;
                account.Images = accountView.Images;
                account.Phone = accountView.Phone;
                account.StationId = accountView.StationId;
                account.RoleId = (byte)accountView.RoleId;
                account.Dob = accountView.Dob;
                account.Name = accountView.Name;
                if (Update(account.Id, account).Result) return account.Id;
                return (int)CheckError.ErrorOrther;
            }
            return check;
        }

        private int CheckModify(AccountView accountView)
        {
            try
            {
                Account accountEmail = GetDataACE().SingleOrDefault(s => s.Id != accountView.Id && s.Email.Trim().ToLower() == accountView.Email.Trim().ToLower());
                if (accountEmail != null)
                {
                    return (int)CheckError.AlreadyEmail;
                }
                Account accountPhone = GetDataACE().SingleOrDefault(s => s.Id != accountView.Id && s.Phone.Trim() == accountView.Phone.Trim());
                if (accountPhone != null)
                {
                    return (int)CheckError.AlreadyPhone;
                }
                return (int)CheckError.Success;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return (int)CheckError.ErrorOrther;
            }
        }

        public bool SetStatus(int id)
        {
            Account account = GetById(id).Result;
            account.Status = !account.Status;
            return Update(account.Id, account).Result;
        }

        public bool SetActive(int id)
        {
            Account account = GetById(id).Result;
            account.Active = !account.Active;
            return Update(account.Id, account).Result;
        }

        #endregion

        #region Remove
        public bool RemoveACE(int id)
        {
            return Delete(id).Result;
        }

        #endregion

        #region Search Paging
        public List<AccountView> Search(int page, string textsearch, int search_case)
        {
            int start = size * (page - 1);
            string columnSearch = "";
            switch (search_case)
            {
                case (int)SearchUser.Name:
                    columnSearch = "[name]";
                    break;
                case (int)SearchUser.Address:
                    columnSearch = "[address]";
                    break;
                case (int)SearchUser.Code:
                    columnSearch = "[code]";
                    break;
                case (int)SearchUser.Email:
                    columnSearch = "[email]";
                    break;
                case (int)SearchUser.Phone:
                    columnSearch = "[phone]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [account] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1")
                .Skip(start)
                .Take(size)
                .Select(s => new AccountView
                {
                    Id = s.Id,
                    Active = (bool)s.Active,
                    RoleName = s.Role.Name,
                    Name = s.Name,
                    Address = s.Address,
                    Code = s.Code,
                    DayCreate = (DateTime)s.DayCreate,
                    DayEdited = (DateTime)s.DayEdited,
                    Description = s.Description,
                    Dob = (DateTime)s.Dob,
                    EditerId = (int)s.EditerId,
                    Email = s.Email,
                    Gender = (int)s.Gender,
                    Images = s.Images,
                    Password = s.Password,
                    Phone = s.Phone,
                    RoleId = s.Role.Id,
                    StationId = (int)s.StationId,
                    StationName = s.Station.Name
                }).ToList();
        }

        public int CountSearchData(string textsearch, int search_case)
        {
            string columnSearch = "";
            switch (search_case)
            {
                case (int)SearchUser.Name:
                    columnSearch = "[name]";
                    break;
                case (int)SearchUser.Address:
                    columnSearch = "[address]";
                    break;
                case (int)SearchUser.Code:
                    columnSearch = "[code]";
                    break;
                case (int)SearchUser.Email:
                    columnSearch = "[email]";
                    break;
                case (int)SearchUser.Phone:
                    columnSearch = "[phone]";
                    break;
            }
            return GetDataRawSqlACE($"SELECT * FROM [account] WHERE {columnSearch} {search} like  N'%{textsearch}%' AND [status] = 1").Count();
        }

        #endregion

        #region ForgotPW
        public string InsertCodeForgotPW(string email_phone)
        {
            try
            {
                Account account = GetDataACE().SingleOrDefault(s =>
                s.Email.ToLower() == email_phone.Trim().ToLower() ||
                s.Phone.ToLower() == email_phone.Trim().ToLower());
                if (account == null) return null;
                account.ForgotPass = Guid.NewGuid().ToString("n").Substring(0, 8);
                bool a = Update(account.Id, account).Result;
                return account.Email + "-" + account.ForgotPass;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public AccountView CompareCodeChangePW(string email_phone, string code)
        {
            AccountView account = GetDataACE().Where(s =>
                s.Email.ToLower() == email_phone.Trim().ToLower() &&
                s.ForgotPass == code).Select(s => new AccountView
                {
                    Id = s.Id,
                    Password = s.Password,
                }).SingleOrDefault();
            return account;
        }

        public bool UpdatePassword(AccountView accountView)
        {
            Account account = GetById(accountView.Id).Result;
            account.Password = MD5ACE.Encrypt(accountView.Password);
            account.ForgotPass = null;
            return Update(account.Id, account).Result;
        }

        #endregion
    }
}
