using AutoMapper;
using Data.DTOs;
using Data;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryDTO>();

            CreateMap<List<Category>, List<CategoryDTO>>()
                .ConvertUsing(src => src.Select(c => new CategoryDTO
                {
                    CategoryCode = c.CategoryCode,
                    CategoryName = c.CategoryName,
                    State = c.State,
                }).ToList());
        }    
    }
}
