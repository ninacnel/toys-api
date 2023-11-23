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

            CreateMap<List<toys>, List<ToyDTO>>()
                .ConvertUsing(src => src.Select(t => new ToyDTO
                {
                    code = t.code,
                    name = t.name,
                    category_id = t.category_id,
                    description = t.description,
                    stock = t.stock,
                    stock_threshold = t.stock_threshold,
                    state = t.state,
                    price = t.price,
                }).ToList());
        }
    }
}
