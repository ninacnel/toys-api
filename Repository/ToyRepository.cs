using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ToyRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public ToyRepository(toystoreContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<ToyDTO> GetToys()
        {
            var toys = _context.toys.ToList();
            var response = _mapper.Map<List<ToyDTO>>(toys);
            return response;
        }

        public ToyDTO AddToy(ToyViewModel toy)
        {
            ToyDTO newToy = new ToyDTO();

            _context.toys.Add(new toys()
            {
                code = toy.code,
                name = toy.name,
                category_id = toy.category_id,
                description = toy.description,
                stock = toy.stock,
                stock_threshold = toy.stock_threshold,
                state = toy.state,
                price = toy.price,
            });

            _context.SaveChanges();

            newToy.code = toy.code;
            newToy.name = toy.name;
            newToy.category_id = toy.category_id;
            newToy.description = toy.description;
            newToy.state = toy.state;
            newToy.stock = toy.stock;
            newToy.stock_threshold = toy.stock_threshold;
            newToy.price = toy.price;

            return newToy;
        }
    }
}
