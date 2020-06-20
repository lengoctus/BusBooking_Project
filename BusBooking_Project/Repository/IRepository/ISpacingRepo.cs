using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface ISpacingRepo : IGenericRepo<Spacing>
    {
        int CreateACE(SpacingView spacingView);
        int ModifyACE(SpacingView spacingView);
        bool RemoveACE(int id);
        List<SpacingView> GetData(int page);
        SpacingView GetByIdACE(int id);
        int CountData();
        List<SpacingView> SearchData(int page, string textsearch, int case_search);
        int CountSearchData(string textsearch, int case_search);
        bool SetActive(int id);
        bool SetStatus(int id);
    }
}
