using Data.DTOs;
using Repository;
using Services.IServices;

namespace Services.Services
{
    public class StockService : IStockService
    {
        private readonly StockRepository _repository;

        public StockService(StockRepository repository)
        {
            _repository = repository;
        }

        public ToyDTO AddStock(int toycode)
        {
            return _repository.AddStock(toycode);
        }
    }
}
