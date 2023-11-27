using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<categories, CategoryDTO>();

            CreateMap<List<categories>, List<CategoryDTO>>()
                .ConvertUsing(src => src.Select(c => new CategoryDTO
                {
                    category_code = c.category_code,
                    category_name = c.category_name,
                    state = c.state,
                }).ToList());
        }    
    }
}
