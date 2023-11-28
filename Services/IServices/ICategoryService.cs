using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICategoryService
    {
        List<CategoryDTO> GetCategories();
        string GetCategoryById(int id);
        CategoryDTO AddCategory(CategoryViewModel category);
        CategoryDTO UpdateCategory(CategoryViewModel category);
        void DeleteCategory(int id);
        void SoftDeleteCategory(int id);
        void RecoverCategory(int id);
    }
}
