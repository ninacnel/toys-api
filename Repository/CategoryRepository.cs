using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using System.Xml.Linq;

namespace Repository
{
    public class CategoryRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(toystoreContext context)
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

        public string GetCategoryById(int id)
        {
            var category = _context.categories.FirstOrDefault(c => c.category_code == id);
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            var response = categoryDTO.category_name;
            return response;
        }

        public CategoryDTO AddCategory(CategoryViewModel category)
        {
            CategoryDTO newCategory = new CategoryDTO();

            _context.categories.Add(new categories()
            {
                category_name = category.category_name,
            });

            _context.SaveChanges();

            newCategory.category_name = category.category_name;

            return newCategory;
        }

        public CategoryDTO UpdateCategory(CategoryViewModel category)
        {
            categories categoryDB = _context.categories.Single(c => c.category_code == category.category_code);
            CategoryDTO newCategory = new CategoryDTO();

            categoryDB.category_name = category.category_name;

            _context.SaveChanges();

            newCategory.category_name = category.category_name;

            return newCategory;
        }

        public void DeleteCategory(int id)
        {
            _context.categories.Remove(_context.categories.Single(c => c.category_code == id));
            _context.SaveChanges();
        }

        public void SoftDeleteCategory(int id)
        {
            categories category = _context.categories.Single(c => c.category_code == id);
            if (category.state == true)
            {
                category.state = false;
            }
            _context.SaveChanges();
        }
        public void RecoverCategory(int id)
        {
            categories category = _context.categories.Single(c => c.category_code == id);
            if (category.state == false)
            {
                category.state = true;
            }
            _context.SaveChanges();
        }
    }
}
