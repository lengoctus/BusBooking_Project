using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using BusBooking_Project.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.CsRepository
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        private readonly ConnectDbContext _db;
        public CategoryRepo(ConnectDbContext db) : base(db)
        {
            _db = db;
        }

        #region GetAllCategory
        public List<CategoryView> GetAllCategory()
        {
            return GetAll().Result.Where(p => p.Status == true).Select(p => new CategoryView
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Active = p.Active ?? false,
                Status = p.Status ?? false,
                Code = p.Code
            }).OrderBy(p => p.Id).ToList();

        }
        #endregion


        #region CheckIsExists
        public override bool CheckIsExists(Category category)
        {
            try
            {
                var cate = GetAll().Result.AsNoTracking().FirstOrDefault(p => (p.Code.ToLower() == category.Code.ToLower()) && p.Status == true);
                if (cate != null)
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
        #endregion


        #region Create Category
        public CategoryView CreateCate(Category category)
        {
            var rs = Create(category, CheckIsExists(category)).Result;
            if (rs != null)
            {
                return new CategoryView
                {
                    Code = rs.Code,
                    Name = rs.Name,
                    Price = rs.Price,
                    Active = rs.Active ?? true,
                    Status = rs.Status ?? true
                };
            }
            return null;
        }
        #endregion

        #region Get CategoryView by Id
        public CategoryView GetByidCate(int idCate)
        {
            var category = GetById(idCate).Result;
            if (category != null)
            {
                return new CategoryView
                {
                    Code = category.Code,
                    Name = category.Name,
                    Price = category.Price,
                    Active = category.Active ?? true,
                    Status = category.Status ?? true
                };
            }
            return null;
        }
        #endregion


        #region Update Category
        public bool UpdateCategory(int Id, CategoryView categoryView)
        {
            var checkCate = GetAll().Result.FirstOrDefault(p => (p.Code.ToLower() == categoryView.Code.ToLower()) && p.Status == true);
            if (checkCate != null)
            {
                return false;
            }

            var findCate = Update(Id, new Category { Id = categoryView.Id, Name = categoryView.Name, Active = categoryView.Active, Status = categoryView.Status, Code = categoryView.Code, Price = categoryView.Price }).Result;

            if (findCate)
            {
                return true;
            }
            return false;
        }
        #endregion


        #region Update Status Category
        public bool UpdateStatus(int[] arrCateId)
        {
            if (arrCateId.Length > 0)
            {
                var listCategory = new List<Category>();

                foreach (var id in arrCateId)
                {
                    var checkBus = _db.Bus.FirstOrDefault(bus => bus.CateId == id);
                    if (checkBus != null)
                    {
                        return false;
                    }

                    var getCate = GetById(id).Result;
                    if (getCate == null)
                    {
                        return false;
                    }
                    getCate.Status = false;
                    listCategory.Add(getCate);

                }
                if (listCategory.Count > 0)
                {
                    var rs = UpdateMultiField(listCategory).Result;
                    if (rs)
                    {
                        return true;
                    }
                }

            }
            return false;

        }
        #endregion
    }
}
