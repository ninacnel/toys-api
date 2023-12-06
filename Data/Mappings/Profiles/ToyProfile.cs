using AutoMapper;
using Data.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings.Profiles
{
    public class ToyProfile : Profile
    {
        public ToyProfile()
        {
            CreateMap<toys, ToyDTO>();

            CreateMap<toys, ToyDTO>()
                .ForMember(dest => dest.toy_img, opt => opt.MapFrom(src => src.toy_img))
                .ForMember(dest => dest.PriceHistory, opt => opt.MapFrom(src => src.price_history != null ? src.price_history.ToList() : null));

            CreateMap<List<toys>, List<ToyDTO>>()
                .ConvertUsing(src => src.Select(t => new ToyDTO
                {
                    code = t.code,
                    name = t.name,
                    category_id = t.category_id,
                    description = t.description,
                    toy_img = t.toy_img,
                    stock = t.stock,
                    stock_threshold = t.stock_threshold,
                    state = t.state,
                    price = t.price,
                }).ToList());

            CreateMap<toys, ToyDTO>()
                .ForMember(dest => dest.toy_img, opt => opt.MapFrom(src => ConvertImageToBase64(src.toy_img)));

            
        }
        private string ConvertImageToBase64(byte[] imageBytes)
        {
            return imageBytes != null ? Convert.ToBase64String(imageBytes) : null;
        }
    }
}