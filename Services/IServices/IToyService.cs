using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
