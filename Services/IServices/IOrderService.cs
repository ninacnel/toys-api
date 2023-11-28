using Data.DTOs;
using Data.ViewModels;

namespace Services.IServices
{
    public interface IOrderService
    {
        List<OrderDTO> GetOrders();
        OrderDTO GetOrderById(int id);
        OrderDTO AddOrder(OrderViewModel order);
        OrderDTO UpdateOrder(OrderViewModel order);
        OrderDTO ModifyToyCode(/*int id, */OrderLineViewModel orderLine);
        void DeleteOrder(int id);
        void SoftDeleteOrder(int id);
        void RecoverOrder(int id);
    }
}
