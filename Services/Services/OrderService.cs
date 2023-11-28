using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

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

        public OrderDTO ModifyToyCode(/*int id, */OrderLineViewModel orderLine)
        {
            return _repository.ModifyToyCode(orderLine);
        }

        public void DeleteOrder(int id)
        {
            _repository.DeleteOrder(id);
        }
        public void SoftDeleteOrder(int id)
        {
            _repository.SoftDeleteOrder(id);
        }
        public void RecoverOrder(int id)
        {
            _repository.RecoverOrder(id);
        }
    }
}
