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
        string ChangePhoto(int id, byte[] newPhoto);
        ToyDTO ChangePrice(int id, int newPrice);
        void DeleteToy(int id);
        void SoftDeleteToy(int id);
        void RecoverToy(int id);
    }
}
