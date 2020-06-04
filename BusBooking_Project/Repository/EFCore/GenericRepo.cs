using BusBooking_Project.Models.Entities;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.EFCore
{
    public abstract class GenericRepo<T> : IGenericRepo<T> where T : class, IEntity
    {
        private readonly ConnectDbContext _db;

        public GenericRepo(ConnectDbContext db)
        {
            _db = db;
        }

        /// =============================================== Create ======================================
        /// <summary>
        /// Create entity.
        /// Before creating a new entity that is required to check if an entity already exists (email, phone, code, ...).
        /// If Checkvalue is false will insert success
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="Checkvalue"></param>
        /// <returns>Entity</returns>
        public async Task<T> Create(T entity, bool Checkvalue)
        {
            try
            {
                if (Checkvalue)
                {
                    return await Task.FromResult<T>(null);
                }
                await _db.Set<T>().AddAsync(entity);
                await _db.SaveChangesAsync();

                return await Task.FromResult(entity);
            }
            catch (Exception e)
            {
                var error = e.Message;
                return await Task.FromResult<T>(null);
            }
        }

        /// =============================================== Check Exists ======================================
        /// <summary>
        /// Check the existence of entity before the insert.
        /// Note: The class that contains the override method must pull this method into the interface the class is implements 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract bool CheckIsExists(T entity);



        /// =============================================== Delete ======================================
        /// <summary>
        /// Delete Entity using Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>boolean</returns>
        public async Task<bool> Delete(int Id)
        {
            try
            {
                //if (await _db.Set<T>().SingleOrDefaultAsync())
                //{

                //}
                _db.Set<T>().Remove(_db.Set<T>().SingleOrDefault(p => p.Id == Id));
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                var error = e.Message;
                return await Task.FromResult(false);
            }
        }

        /// =============================================== Get All ======================================
        /// <summary>
        /// Get All properties of entity
        /// </summary>
        /// <returns>IQueryable Entity</returns>
        public async Task<IQueryable<T>> GetAll()
        {
            try
            {
                return await Task.FromResult(_db.Set<T>().AsNoTracking());
            }
            catch (Exception e)
            {
                var error = e.Message;
                return await Task.FromResult<IQueryable<T>>(null);
            }
        }


        /// =============================================== Get By Id ======================================
        /// <summary>
        /// Get entity using Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Entity</returns>
        public async Task<T> GetById(int Id)
        {
            try
            {
                var test1 = Thread.CurrentThread.ManagedThreadId;
                var entity = await _db.Set<T>().FirstOrDefaultAsync(p => p.Id == Id);

                var test2 = Thread.CurrentThread.ManagedThreadId;
                return await Task.FromResult(entity);
            }
            catch (Exception e)
            {
                var error = e.Message;
                return await Task.FromResult<T>(null);
            }
        }


        /// =============================================== Update ======================================
        /// <summary>
        /// Update entity:
        /// Step 1: Find entity using Id.
        /// If not found will return false.
        /// Step 2: Update entity and return true.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="entity"></param>
        /// <returns>boolean</returns>
        public async Task<bool> Update(int Id, T entity)
        {
            try
            {
                if (await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == Id) != null)
                {
                    _db.Set<T>().Update(entity);
                    await _db.SaveChangesAsync();

                    return await Task.FromResult(true);
                }
                return await Task.FromResult(false);
            }
            catch (Exception e)
            {
                var error = e.InnerException.Message;
                return await Task.FromResult(false);
            }
        }

        public IQueryable<T> GetDataRawSqlACE(string query)
        {
            return _db.Set<T>().FromSqlRaw(query).AsNoTracking();
        }

        public IQueryable<T> GetDataACE()
        {
            return _db.Set<T>().AsNoTracking();
        }
    }
}
