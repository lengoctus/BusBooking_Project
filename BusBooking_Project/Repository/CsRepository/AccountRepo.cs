﻿using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class AccountRepo : GenericRepo<Account>, IAccountRepo
    {

        public AccountRepo(ConnectDbContext db) : base(db)
        {

        }


        // ================================================= CheckIsExists =================================
        public override bool CheckIsExists(Account account)
        {
            try
            {
                var entity = GetAll().Result.SingleOrDefault(p => p.Email == account.Email);
                if (entity != null)
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

        public AccountView Login(Account account)
        {
            return GetAll().Result.Where(s => s.Email.ToLower().Trim() == account.Email.ToLower().Trim() && s.Password.ToLower().Trim() == account.Password.ToLower().Trim()).Select(s => new AccountView
            {
                Id = s.Id,
                Name = s.Name,
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
                Role = s.Role,
                Stastus = (bool)s.Status,
                EditerId = (int)s.EditerId
            }).SingleOrDefault();
        }
    }
}
