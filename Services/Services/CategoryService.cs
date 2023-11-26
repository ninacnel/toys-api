using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public List<CategoryDTO> GetCategories()
        {
            return _repository.GetCategories();
        }

        public CategoryDTO GetCategoryById(int id)
        {
            return _repository.GetCategoryById(id);
        }
        public CategoryDTO AddCategory(CategoryViewModel category)
        {
            return _repository.AddCategory(category);
        }
        public CategoryDTO UpdateCategory(CategoryViewModel category)
        {
            return _repository.UpdateCategory(category);
        }
        void DeleteCategory(int id)
        {
            _repository.DeleteCategory(id);
        }
        //void SoftDeleteCategory(int id)
        //{
        //    _repository.SoftDeleteCategory(id);
        //}
        //void RecoverCategory(int id)
        //{
        //    _repository.RecoverCategory(id);
        //}
    }
}
