using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

namespace Services.Services
{
    public class ToyService : IToyService
    {
        private readonly ToyRepository _repository;

        public ToyService(ToyRepository repository)
        {
            _repository = repository;
        }
        public List<ToyDTO> GetToys()
        {
            return _repository.GetToys();
        }
        public ToyDTO GetToyById(int id)
        {
            return _repository.GetToyById(id);
        }
        public ToyDTO AddToy(ToyViewModel toy)
        {
            return _repository.AddToy(toy);
        }
        public ToyDTO UpdateToy(ToyViewModel toy)
        {
            return _repository.UpdateToy(toy);
        }
        public ToyDTO ChangePrice(int id, int newPrice)
        {
            return _repository.ChangePrice(id, newPrice);
        }
        public string ChangePhoto(int id, byte[] newPhoto)
        {
            return _repository.ChangePhoto(id, newPhoto);
        }
        public void DeleteToy(int id)
        {
            _repository.DeleteToy(id);
        }
        public void SoftDeleteToy(int id)
        {
            _repository.SoftDeleteToy(id);
        }
        public void RecoverToy(int id)
        {
            _repository.RecoverToy(id);
        }
    }
}