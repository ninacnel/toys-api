using AutoMapper;
using Data;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;

namespace Repository
{
    public class StockRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StockRepository(DataContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public ToyDTO AddStock(int toycode)
        {
            var toyDB = _context.toys.Single(t => t.Code == toycode);

            toyDB.Stock += 50;

            var toyDTO = _mapper.Map<ToyDTO>(toyDB);

            _context.SaveChanges();

            return toyDTO;
        }

        public void DecreaseStock(OrderViewModel order)
        {
            foreach (var orderLineViewModel in order.OrderLines)
            {
                var toy = _context.toys.Single(t => t.Code == orderLineViewModel.ToyCode);
                toy.Stock -= orderLineViewModel.Quantity;
            }

            _context.SaveChanges();
        }

        public bool CheckStock(int? toycode, OrderLineDTO orderline)
        {
            var actualStock = _context.toys.Single(t => t.Code == toycode).Stock;

            var threshold = _context.toys.Single(t => t.Code == toycode).StockThreshold;

            if (actualStock > threshold)
            {
                if (actualStock > orderline.Quantity)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
