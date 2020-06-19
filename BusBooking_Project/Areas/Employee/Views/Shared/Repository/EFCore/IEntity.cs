using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.EFCore
{
    public interface IEntity
    {
        public int Id { get; set; }
    }
}
