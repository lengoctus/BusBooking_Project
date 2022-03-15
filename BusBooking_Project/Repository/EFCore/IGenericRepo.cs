using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.EFCore
{
    public interface IGenericRepo<T> where T : class
    {

        Task<IQueryable<T>> GetAll();


        Task<T> Create(T entity, bool Checkvalue);

        Task<T> Add(T entity);


        Task<T> GetById(int Id);

        Task<bool> Update(int Id, T entity);

        Task<bool> DeleteMultiField(int[] Id);

        Task<bool> Delete(int Id);
        Task<bool> UpdateMultiField(List<T> entity);

        /// Của Sáng ở here
        IQueryable<T> GetDataRawSqlACE(string query);

        IQueryable<T> GetDataACE();
        //Của Sáng ở here

    }
}
