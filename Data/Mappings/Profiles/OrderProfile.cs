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
            CreateMap<Order, OrderDTO>();
                //.ForMember(dest => dest.order_date, opt => opt.MapFrom(src => (DateTime?)src.order_date));

            CreateMap<OrderLine, OrderLineDTO>();

            CreateMap<OrderViewModel, OrderDTO>()
                .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

            CreateMap<OrderLineViewModel, OrderLineDTO>();

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderLines, opt => opt.Ignore());

            CreateMap<OrderLineDTO, OrderLine>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore());

            CreateMap<List<OrderLineDTO>, List<OrderLine>>()
                .ConvertUsing((src, dest, context) =>
                 {
                     var orderLines = new List<OrderLine>();

                     foreach (var orderLineDto in src)
                     {
                         orderLines.Add(context.Mapper.Map<OrderLine>(orderLineDto));
                     }

                     return orderLines;
                 });
        }
    }
}
