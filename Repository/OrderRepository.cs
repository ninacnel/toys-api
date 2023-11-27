using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(toystoreContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<OrderDTO> GetOrders()
        {
            var orders = _context.orders.ToList();
            var response = _mapper.Map<List<OrderDTO>>(orders);
            return response;
        }

        public OrderDTO AddOrder(OrderViewModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.total_amount = CalculateTotalAmount(order.order_lines);

            orderDTO.order_lines = MapOrderLines(order.order_lines);

            _context.orders.Add(_mapper.Map<orders>(orderDTO));

            var orderLinesDTO = MapOrderLines(order.order_lines);
            foreach (var orderLineEntity in _mapper.Map<List<order_line>>(orderLinesDTO))
            {
                //orderEntity.order_lines.Add(orderLineEntity);
                _context.order_line.Add(orderLineEntity);
            }

            _context.SaveChanges();

            return orderDTO;
        }

        private decimal CalculateTotalAmount(List<OrderLineViewModel> orderLines)
        {
            decimal totalAmount = 0;

            foreach (var line in orderLines)
            {
                totalAmount += line.quantity * line.price;
            }

            return totalAmount;
        }
        private List<OrderLineDTO> MapOrderLines(List<OrderLineViewModel> orderLines) 
        {
            return _mapper.Map<List<OrderLineDTO>>(orderLines);
        }
    }
}
