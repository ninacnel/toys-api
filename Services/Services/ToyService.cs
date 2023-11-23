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

        public ToyDTO AddToy(ToyViewModel toy)
        {
            return _repository.AddToy(toy);
        }

    }
}
