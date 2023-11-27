using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _repository;

        public OrderService(OrderRepository repository)
        {
            _repository = repository;
        }

        public List<OrderDTO> GetOrders()
        {
            return _repository.GetOrders();
        }

        public OrderDTO GetOrderById(int id)
        {
            return _repository.GetOrderById(id);
        }

        public OrderDTO AddOrder(OrderViewModel order)
        {
            return _repository.AddOrder(order);
        }

        public OrderDTO UpdateOrder(OrderViewModel order)
        {
            return _repository.UpdateOrder(order);
        }

        public OrderDTO ModifyProductCode(/*int id, */OrderLineViewModel orderLine)
        {
            return _repository.ModifyProductCode(orderLine);
        }
    }
}
