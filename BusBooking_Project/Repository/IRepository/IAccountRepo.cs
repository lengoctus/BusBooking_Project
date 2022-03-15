using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface IAccountRepo : IGenericRepo<Account>
    {
        bool CheckIsExists(Account account);

        AccountView Login(Account account);

        AccountView GetByIdACE(int id);

        int CountSearchData(string textsearch, int search_case);

        List<AccountView> GetData(int page);

        List<AccountView> Search(int page, string textsearch, int search_case);

        int CountData();

        int CreateACE(AccountView accountView);

        int ModifyACE(AccountView accountView, int EditerId);

        bool RemoveACE(int id);

        bool SetStatus(int id);
        bool SetActive(int id);

        string InsertCodeForgotPW(string email_phone);

        AccountView CompareCodeChangePW(string email_phone, string code);

        bool UpdatePassword(AccountView account);
        int CreateCustomer(AccountView accountView);
    }
}
