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
        ToyDTO AddToy(ToyViewModel toy);
    }
}
