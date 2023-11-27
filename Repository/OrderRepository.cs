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

        public OrderDTO UpdateOrder(OrderViewModel order)
        {
            orders orderDB = _context.orders.Single(o => o.order_id == order.order_id);
            OrderDTO newOrder = new OrderDTO();

            orderDB.client_id = order.client_id;
            orderDB.order_date = order.order_date;
            orderDB.total_amount = order.total_amount;
            orderDB.state = order.state;

            _context.SaveChanges();

            newOrder.client_id = order.client_id;
            newOrder.order_date = order.order_date;
            newOrder.total_amount = order.total_amount;
            newOrder.state = order.state;

            return newOrder;
            //even if some fields are required, like order_line,
            //if they're not mapped here and then saved in ddbb they won't be modified
        }

        public OrderDTO ModifyToyCode(/*int id, */OrderLineViewModel orderLine)
        {
            // Find the specific order_line based on order_id and order_line_id
            order_line orderLineDB = _context.order_line.SingleOrDefault(ol => ol.order_id == orderLine.order_id && ol.order_line_id == orderLine.order_line_id);

            if (orderLineDB == null)
            {
                // Handle the case where the order_line is not found
                // You can throw an exception, return an error, or handle it as needed
                throw new Exception("Order line not found");
            }

            // Modify the toy_code property
            orderLineDB.toy_code = orderLine.toy_code;

            // Save changes to the database
            _context.SaveChanges();

            // Create and return the OrderDTO (if needed)
            OrderDTO orderLineChanged = new OrderDTO();

            // Populate orderLineChanged with the modified order_line or any other necessary information

            return orderLineChanged;
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

        public void DeleteOrder(int id)
        {
            _context.orders.Remove(_context.orders.Single(o => o.order_id == id));
            _context.SaveChanges();
        }
        public void SoftDeleteOrder(int id)
        {
            orders order = _context.orders.Single(o => o.order_id == id);
            if (order.state == true)
            {
                order.state = false;
            }
            _context.SaveChanges();
        }
        public void RecoverOrder(int id)
        {
            orders order = _context.orders.Single(o => o.order_id == id);
            if (order.state == false)
            {
                order.state = true;
            }
            _context.SaveChanges();
        }
    }
}
