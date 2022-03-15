using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.SupportsTu
{
    public sealed class PaginationSupport
    {
        public static readonly int pagezise = 3;

        public static int GetRows(int nbPage)
        {
            return pagezise * (nbPage - 1);
        }
    }
}