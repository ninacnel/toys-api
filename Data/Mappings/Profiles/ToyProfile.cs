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
            CreateMap<Toy, ToyDTO>();

            CreateMap<Toy, ToyDTO>()
                .ForMember(dest => dest.ToyImg, opt => opt.MapFrom(src => src.ToyImg))
                .ForMember(dest => dest.PriceHistory, opt => opt.MapFrom(src => src.PriceHistory != null ? src.PriceHistory.ToList() : null));

            CreateMap<List<Toy>, List<ToyDTO>>()
                .ConvertUsing(src => src.Select(t => new ToyDTO
                {
                    Code = t.Code,
                    Name = t.Name,
                    CategoryId = t.CategoryId,
                    Description = t.Description,
                    ToyImg = t.ToyImg,
                    Stock = t.Stock,
                    StockThreshold = t.StockThreshold,
                    State = t.State,
                    Price = t.Price,
                }).ToList());

            CreateMap<Toy, ToyDTO>()
                .ForMember(dest => dest.ToyImg, opt => opt.MapFrom(src => ConvertImageToBase64(src.ToyImg)));

            CreateMap<Toy, ToyDTO>()
                .ForMember(dest => dest.ToyImg, opt => opt.MapFrom(src => src.ToyImg))
                .ForMember(dest => dest.PriceHistory, opt => opt.MapFrom(src => src.PriceHistory.Select(ph => new PriceDTO
                {
                    ToyCode = src.Code,
                    Price = ph.Price,
                    ChangeDate = ph.ChangeDate
                }).ToList()));

        }
        private string ConvertImageToBase64(byte[] imageBytes)
        {
            return imageBytes != null ? Convert.ToBase64String(imageBytes) : null;
        }
    }
}