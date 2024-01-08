using AutoMapper;
using Data;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class OrderRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly EmailRepository _email;
        private readonly StockRepository _stock;

        public OrderRepository(DataContext context, EmailRepository email, StockRepository stock)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _email = email;
            _stock = stock;
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
                .Where(o => o.OrderId == id)
                .Include(od => od.OrderLines)
                .FirstOrDefault();

            var orderDTO = _mapper.Map<OrderDTO>(orderAndDetails);

            // Convert the HashSet<price_history> to List<price_history>
            var details = orderAndDetails.OrderLines.ToList();

            // Map the price history to a list of PriceDTO
            orderDTO.OrderLines = _mapper.Map<List<OrderLineDTO>>(details);

            return orderDTO;
        }
        public OrderDTO AddOrder(OrderViewModel order)
        {
            var orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.TotalAmount = CalculateTotalAmount(order.OrderLines);

            // Map order and add it to the context
            var orderEntity = _mapper.Map<Order>(orderDTO);
            _context.orders.Add(orderEntity);
            //check stock availability

            foreach (var line in order.OrderLines)
            {
                if (!_stock.CheckStock(line.ToyCode, line))
                {
                    // Rollback changes and return null if at least one toy doesn't have sufficient stock
                    _context.Entry(orderEntity).State = EntityState.Detached;
                    return null;
                }
            }

            _context.SaveChanges();  // Save to get the generated order_id

            // Get the generated order_id
            var orderId = orderEntity.OrderId;

            // Reset order_line_id for each new order

            var nextOrderLineId = 1;

            // Set the order_id and order_line_id for each line manually
            foreach (var orderLineViewModel in order.OrderLines)
            {
                var orderLineEntity = new OrderLine
                {
                    OrderId = orderId,
                    OrderLineId = nextOrderLineId,
                    ToyCode = orderLineViewModel.ToyCode,
                    Quantity = orderLineViewModel.Quantity,
                    Price = orderLineViewModel.Price,
                    SubTotal = orderLineViewModel.Quantity * orderLineViewModel.Price,
                    Wrapped = orderLineViewModel.Wrapped
                };

                _context.ordersLines.Add(orderLineEntity);
                nextOrderLineId++;
            }

            _context.SaveChanges();

            // Decrease stock for each toy in order lines
            _stock.DecreaseStock(order);

            var clientEmail = _context.users.Single(u => u.UserId == orderEntity.ClientId).Email;

            var emailDto = new EmailDto
            {
                To = clientEmail,
                Subject = "Order Created",
                Body = $"Your order has been successfully created. Total Amount ${orderDTO.TotalAmount}"
            };

            _email.SendEmail(emailDto);

            return orderDTO;

        }

        public OrderDTO UpdateOrder(OrderViewModel order)
        {
            Order orderDB = _context.orders.Single(o => o.OrderId == order.OrderId);
            OrderDTO newOrder = new OrderDTO();

            orderDB.ClientId = order.ClientId;
            orderDB.OrderDate = (DateTime)order.OrderDate;
            orderDB.TotalAmount = order.TotalAmount;
            orderDB.State = order.State;

            _context.SaveChanges();

            newOrder.ClientId = order.ClientId;
            newOrder.OrderDate = order.OrderDate;
            newOrder.TotalAmount = order.TotalAmount;
            newOrder.State = order.State;

            return newOrder;
            //even if some fields are required, like order_line,
            //if they're not mapped here and then saved in ddbb they won't be modified
        }

        public OrderDTO ModifyToyCode(/*int id, */OrderLineViewModel orderLine)
        {
            // Find the specific order_line based on order_id and order_line_id
            OrderLine orderLineDB = _context.ordersLines.SingleOrDefault(ol => ol.OrderId == orderLine.OrderId && ol.OrderLineId == orderLine.OrderLineId);

            if (orderLineDB == null)
            {
                // Handle the case where the order_line is not found
                // You can throw an exception, return an error, or handle it as needed
                throw new Exception("Order line not found");
            }

            // Modify the toy_code property
            orderLineDB.ToyCode = orderLine.ToyCode;

            // Save changes to the database
            _context.SaveChanges();

            // Create and return the OrderDTO (if needed)
            OrderDTO orderLineChanged = new OrderDTO();

            // Populate orderLineChanged with the modified order_line or any other necessary information

            return orderLineChanged;
        }

        public void DeleteOrder(int id)
        {
            _context.orders.Remove(_context.orders.Single(o => o.OrderId == id));
            _context.SaveChanges();
        }
        public void SoftDeleteOrder(int id)
        {
            Order order = _context.orders.Single(o => o.OrderId == id);
            if (order.State == true)
            {
                order.State = false;
            }
            _context.SaveChanges();
        }
        public void RecoverOrder(int id)
        {
            Order order = _context.orders.Single(o => o.OrderId == id);
            if (order.State == false)
            {
                order.State = true;
            }
            _context.SaveChanges();
        }

        //helpful methods
        private decimal CalculateTotalAmount(List<OrderLineDTO> orderLines)
        {
            decimal totalAmount = 0;

            foreach (var line in orderLines)
            {
                totalAmount += line.Quantity * line.Price;
            }

            return totalAmount;
        }
    }
}
