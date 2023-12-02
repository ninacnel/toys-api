﻿using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using MailKit.Net.Imap;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ToyRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;
        private readonly CategoryRepository _category;

        public ToyRepository(toystoreContext context, CategoryRepository category)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _category = category;
        }

        public List<ToyDTO> GetToys()
        {
            var toys = _context.toys.ToList();
            var response = _mapper.Map<List<ToyDTO>>(toys);

            foreach (var toyDTO in response)
            {
                toyDTO.category_name = _category.GetCategoryById(toyDTO.category_id);
            }

            return response;
        }
        public ToyDTO GetToyById(int id)
        {
            var toyDB = _context.toys.SingleOrDefault(t => t.code == id);

            if (toyDB == null)
            {
                return null;
            }

            var toyCategory = _category.GetCategoryById(id);

            var toyWithPrices = _context.toys
                .Where(t => t.code == id)
                .Include(t => t.price_history)
                .FirstOrDefault();

            var toyDTO = _mapper.Map<ToyDTO>(toyWithPrices);

            // Convert the HashSet<price_history> to List<price_history>
            var priceHistoryList = toyWithPrices.price_history.ToList();

            // Map the price history to a list of PriceDTO
            toyDTO.PriceHistory = _mapper.Map<List<PriceDTO>>(priceHistoryList);

            // Get category name for the toy
            toyDTO.category_name = _category.GetCategoryById(toyDTO.category_id);


            return toyDTO;
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
                state = true,
                price = toy.price,
            });

            _context.SaveChanges();

            newToy.code = toy.code;
            newToy.name = toy.name;
            newToy.category_id = toy.category_id;
            newToy.description = toy.description;
            newToy.state = true;
            newToy.stock = toy.stock;
            newToy.stock_threshold = toy.stock_threshold;
            newToy.price = toy.price;

            return newToy;
        }

        public ToyDTO UpdateToy(ToyViewModel toy)
        {
            toys toyDB = _context.toys.Single(t => t.code == toy.code);
            ToyDTO newToy = new ToyDTO();

            toyDB.name = toy.name;
            toyDB.description = toy.description;
            toyDB.stock = toy.stock;
            toyDB.stock_threshold = toy.stock_threshold;
            toyDB.price = toy.price;
            toyDB.category_id = toy.category_id;

            _context.SaveChanges();

            newToy.name = toy.name;
            newToy.description = toy.description;
            newToy.stock = toy.stock;
            newToy.stock_threshold = toy.stock_threshold;
            newToy.price = toy.price;
            newToy.category_id = toy.category_id;

            //as we're adding a new price we should update the price_history, with a transaction or trigger
            return newToy;
        }

        public ToyDTO ChangePrice(int id, int newPrice)
        {
            // Retrieve the toy with the specified ID
            var toy = _context.toys.SingleOrDefault(t => t.code == id);

            if (toy == null)
            {
                // Handle the case where the toy with the specified ID is not found
                return null;
            }

            // Save the current price to the price_history table
            var priceHistory = new price_history
            {
                toy_code = toy.code,
                price = toy.price,
                change_date = DateTime.Now // You can adjust the date as needed
            };

            _context.price_history.Add(priceHistory);

            // Update the price of the toy
            toy.price = newPrice;

            // Save changes to the database
            _context.SaveChanges();

            // Map the updated toy to ToyDTO
            var response = _mapper.Map<ToyDTO>(toy);

            return response;
        }

        public void DeleteToy(int id)
        {
            _context.toys.Remove(_context.toys.Single(t => t.code == id));
            _context.SaveChanges();
        }
        public void SoftDeleteToy(int id)
        {
            toys toy = _context.toys.Single(t => t.code == id);
            if (toy.state == true)
            {
                toy.state = false;
            }
            _context.SaveChanges();
        }

        public void RecoverToy(int id)
        {
            toys toy = _context.toys.Single(t => t.code == id);
            if (toy.state == false)
            {
                toy.state = true;
            }
            _context.SaveChanges();
        }
    }
}
