using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;

namespace Repository
{
    public class StockRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public StockRepository(toystoreContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public ToyDTO AddStock(int toycode)
        {
            var toyDB = _context.toys.Single(t => t.code == toycode);

            toyDB.stock += 50;

            var toyDTO = _mapper.Map<ToyDTO>(toyDB);

            _context.SaveChanges();

            return toyDTO;
        }

        public void DecreaseStock(OrderViewModel order)
        {
            foreach (var orderLineViewModel in order.order_lines)
            {
                var toy = _context.toys.Single(t => t.code == orderLineViewModel.toy_code);
                toy.stock -= orderLineViewModel.quantity;
            }

            _context.SaveChanges();
        }

        public bool CheckStock(int? toycode, OrderLineViewModel orderline)
        {
            var actualStock = _context.toys.Single(t => t.code == toycode).stock;

            var threshold = _context.toys.Single(t => t.code == toycode).stock_threshold;

            if (actualStock > threshold)
            {
                if (actualStock > orderline.quantity)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
