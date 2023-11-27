using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IOrderService
    {
        List<OrderDTO> GetOrders();
        OrderDTO GetOrderById(int id);
        OrderDTO AddOrder(OrderViewModel order);
        OrderDTO UpdateOrder(OrderViewModel order);
        OrderDTO ModifyProductCode(/*int id, */OrderLineViewModel orderLine);
    }
}
