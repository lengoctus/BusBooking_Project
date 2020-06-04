﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supports
{
    public class ConstantACE
    {
        public static string search = "Collate latin1_general_ci_ai";
        public static int size = 5;
    }

    public enum SearchUser
    {
        Name = 0,
        Email = 1,
        Phone = 2,
        Gender = 3,
        Code = 4,
        Address = 5
    }

    public enum SearchStation
    {
        Name = 1,
        Location = 2,
        Phone = 3
    }

    public enum CheckError
    {
        Success = 0,
        AlreadyEmail = -1,
        AlreadyPhone = -2,
        AlreadyCode = -3,
        ErrorOrther = -4
    }

    public enum BooleanACE
    {
        Off = 0,
        On = 1,
        All = -1
    }
}
