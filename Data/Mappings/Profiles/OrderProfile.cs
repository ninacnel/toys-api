using AutoMapper;
using Data.DTOs;
using Data.Models;
using Data.ViewModels;

namespace Data.Mappings.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<orders, OrderDTO>();

            CreateMap<order_line, OrderLineDTO>();

            CreateMap<OrderViewModel, OrderDTO>()
                .ForMember(dest => dest.order_lines, opt => opt.MapFrom(src => src.order_lines));

            CreateMap<OrderLineViewModel, OrderLineDTO>();

            CreateMap<OrderDTO, orders>()
                .ForMember(dest => dest.order_id, opt => opt.Ignore())
                .ForMember(dest => dest.order_line, opt => opt.Ignore());

            CreateMap<OrderLineDTO, order_line>()
                .ForMember(dest => dest.order_id, opt => opt.Ignore());

            CreateMap<List<OrderLineDTO>, List<order_line>>()
                .ConvertUsing((src, dest, context) =>
                 {
                     var orderLines = new List<order_line>();

                     foreach (var orderLineDto in src)
                     {
                         orderLines.Add(context.Mapper.Map<order_line>(orderLineDto));
                     }

                     return orderLines;
                 });
        }
    }
}
