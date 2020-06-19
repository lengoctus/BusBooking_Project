using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface ISeatRePo : IGenericRepo<Seat>
    {
        bool CheckIsExists(Seat entity);
        bool Delete(int[] arrId);
        List<SeatView> GetAllSeat(int page);
        int CountAllSeat();
        SeatView GetByIdSeat(int Id);
        List<SeatView> SearchByBus(int page, int busid);
        int CountSearchByBus(int busid);
    }
}
