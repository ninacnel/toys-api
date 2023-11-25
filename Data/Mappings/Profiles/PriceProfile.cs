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
    public class PriceProfile : Profile
    {
        public PriceProfile()
        {
            CreateMap<price_history, PriceDTO>();

            //CreateMap<List<price_history>, List<PriceDTO>>()
            //    .ConvertUsing(src => src.Select(p => new PriceDTO
            //    {
            //        history_id = p.history_id,
            //        toy_code = p.toy_code,
            //        change_date = p.change_date,
            //        price = p.price,
            //    }).ToList());
        }
    }
}
