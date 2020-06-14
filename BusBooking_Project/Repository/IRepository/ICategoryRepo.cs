using BusBooking_Project.Models.Entities;
using BusBooking_Project.Models.ModelsView;
using BusBooking_Project.Repository.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBooking_Project.Repository.IRepository
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        bool CheckIsExists(Category category);
        CategoryView CreateCate(Category category);
        bool DeleteMultiCategory(int[] idCate);
        List<CategoryView> GetAllCategory();
        CategoryView GetByidCate(int idCate);
        bool UpdateCategory(int Id, CategoryView categoryView);
    }
}
