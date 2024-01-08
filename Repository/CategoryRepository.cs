using AutoMapper;
using Data;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;

namespace Repository
{
    public class CategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<CategoryDTO> GetCategories()
        {
            var categories = _context.categories.ToList();
            var response = _mapper.Map<List<CategoryDTO>>(categories);
            return response;
        }

        public string GetCategoryById(int? id)
        {
            var category = _context.categories.FirstOrDefault(c => c.CategoryCode == id);
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            var response = categoryDTO.CategoryName;
            return response;
        }

        public CategoryDTO AddCategory(CategoryViewModel category)
        {
            CategoryDTO newCategory = new CategoryDTO();

            _context.categories.Add(new Category()
            {
                CategoryName = category.CategoryName,
                State = true,
            });

            _context.SaveChanges();

            newCategory.CategoryName = category.CategoryName;

            return newCategory;
        }

        public CategoryDTO UpdateCategory(CategoryViewModel category)
        {
            Category categoryDB = _context.categories.Single(c => c.CategoryCode == category.CategoryCode);
            CategoryDTO newCategory = new CategoryDTO();

            categoryDB.CategoryName = category.CategoryName;

            _context.SaveChanges();

            newCategory.CategoryName = category.CategoryName;

            return newCategory;
        }

        public void DeleteCategory(int id)
        {
            _context.categories.Remove(_context.categories.Single(c => c.CategoryCode == id));
            _context.SaveChanges();
        }

        public void SoftDeleteCategory(int id)
        {
            Category category = _context.categories.Single(c => c.CategoryCode == id);
            if (category.State == true)
            {
                category.State = false;
            }
            _context.SaveChanges();
        }
        public void RecoverCategory(int id)
        {
            Category category = _context.categories.Single(c => c.CategoryCode == id);
            if (category.State == false)
            {
                category.State = true;
            }
            _context.SaveChanges();
        }
    }
}
