using Data.DTOs;

namespace Services.IServices
{
    public interface IStockService
    {
        ToyDTO AddStock(int toycode);
    }
}
