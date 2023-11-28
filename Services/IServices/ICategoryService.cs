using Data.DTOs;
using Data.ViewModels;

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
