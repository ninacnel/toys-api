using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        public OrderDTO GetOrderById(int id)
        {
            var orderAndDetails = _context.orders
                .Where(o => o.order_id == id)
                .Include(od => od.order_line)
                .FirstOrDefault();

            var orderDTO = _mapper.Map<OrderDTO>(orderAndDetails);

            // Convert the HashSet<price_history> to List<price_history>
            var details = orderAndDetails.order_line.ToList();

            // Map the price history to a list of PriceDTO
            orderDTO.order_lines = _mapper.Map<List<OrderLineDTO>>(details);

            return orderDTO;
        }
        public OrderDTO AddOrder(OrderViewModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.total_amount = CalculateTotalAmount(order.order_lines);

            // Map order and add it to the context
            var orderEntity = _mapper.Map<orders>(orderDTO);
            _context.orders.Add(orderEntity);
            _context.SaveChanges();  // Save to get the generated order_id

            // Get the generated order_id
            var orderId = orderEntity.order_id;

            // Reset order_line_id for each new order
            
            var nextOrderLineId = 1;

            // Set the order_id and order_line_id for each line manually
            foreach (var orderLineViewModel in order.order_lines)
            {
                var orderLineEntity = new order_line
                {
                    order_id = orderId,
                    order_line_id = nextOrderLineId,
                    toy_code = orderLineViewModel.toy_code,
                    quantity = orderLineViewModel.quantity,
                    price = orderLineViewModel.price,
                    sub_total = orderLineViewModel.sub_total,
                    wrapped = orderLineViewModel.wrapped
                };

                _context.order_line.Add(orderLineEntity);
                nextOrderLineId++;
            }

            _context.SaveChanges();  // Save changes for order lines

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
