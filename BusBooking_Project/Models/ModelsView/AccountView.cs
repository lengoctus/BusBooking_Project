﻿using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Models.ModelsView
{
    public class AccountView : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Images { get; set; }
        public byte Gender { get; set; }
        public DateTime DayCreate { get; set; }
        public DateTime DayEdited { get; set; }
        public Account Editer { get; set; }
        public bool Stastus { get; set; }
        public byte Role { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string ForgotPass { get; set; }
    }
}
