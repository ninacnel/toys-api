using Data.DTOs;
using Data.ViewModels;

namespace Services.IServices
{
    public interface IToyService
    {
        List<ToyDTO> GetToys();
        ToyDTO GetToyById(int id);
        ToyDTO AddToy(ToyViewModel toy);
        ToyDTO UpdateToy(ToyViewModel toy);
        ToyDTO ChangePrice(int id, int newPrice);
        void DeleteToy(int id);
        void SoftDeleteToy(int id);
        void RecoverToy(int id);
    }
}
