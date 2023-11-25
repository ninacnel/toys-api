using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ToyDTO GetToyPricesById(int id)
        {
            return _repository.GetToyPricesById(id);
        }
        public ToyDTO AddToy(ToyViewModel toy)
        {
            return _repository.AddToy(toy);
        }

        public ToyDTO ChangePrice(int id, int newPrice)
        {
            return _repository.ChangePrice(id, newPrice);
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