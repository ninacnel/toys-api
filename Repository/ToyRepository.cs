using AutoMapper;
using Data;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Repository
{
    public class ToyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly CategoryRepository _category;

        public ToyRepository(DataContext context, CategoryRepository category)
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
                toyDTO.CategoryName = _category.GetCategoryById(toyDTO.CategoryId);
            }

            return response;
        }

        public ToyDTO GetToyById(int id)
        {
            var toyExists = _context.toys.SingleOrDefault(t => t.Code == id);

            if (toyExists == null)
            {
                return null;
            }

            var toyWithPrices = _context.toys
                .Where(t => t.Code == id)
                .Include(t => t.PriceHistory)
                .SingleOrDefault();

            var toyDTO = _mapper.Map<ToyDTO>(toyWithPrices);

            // Convert the HashSet<price_history> to List<price_history>
            var priceHistoryList = toyWithPrices.PriceHistory.ToList();

            // Map the price history to a list of PriceDTO
            toyDTO.PriceHistory = _mapper.Map<List<PriceDTO>>(priceHistoryList);

            toyDTO.CategoryName = _category.GetCategoryById(toyDTO.CategoryId);

            return toyDTO;
        }

        public ToyDTO AddToy(ToyViewModel toy)
        {
            var toyExists = _context.toys.SingleOrDefault(t => t.Code == toy.Code);

            if (toyExists != null)
            {
                return null;
            }

            ToyDTO newToy = new ToyDTO();

            if (toy.ImageFile != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        // Read the file data from the IFormFile
                        toy.ImageFile.CopyTo(ms);

                        // Reset the position of the MemoryStream
                        ms.Position = 0;

                        // Save the file bytes to the database
                        Toy newToyDB = new Toy()
                        {
                            Code = toy.Code,
                            Name = toy.Name,
                            CategoryId = toy.CategoryId,
                            Description = toy.Description,
                            ToyImg = ms.ToArray(), // Convert MemoryStream to byte array
                            Stock = toy.Stock,
                            StockThreshold = toy.StockThreshold,
                            State = true,
                            Price = toy.Price,
                        };

                        _context.toys.Add(newToyDB);

                        _context.SaveChanges();
                        // Retrieve the code of the recently created toy
                        int newToyCode = newToyDB.Code;
                        // Reset the position again if needed
                        ms.Position = 0;

                        string category = _category.GetCategoryById(newToyDB.CategoryId);

                        newToy.Code = newToyCode;
                        newToy.Name = toy.Name;
                        newToy.CategoryId = toy.CategoryId;
                        newToy.CategoryName = category;
                        newToy.Description = toy.Description;
                        newToy.ToyImg = ms.ToArray(); // Convert IFormFile to byte array
                        newToy.State = true;
                        newToy.Stock = toy.Stock;
                        newToy.StockThreshold = toy.StockThreshold;
                        newToy.Price = toy.Price;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return newToy;
        }

        public ToyDTO UpdateToy(ToyViewModel toy)
        {
            Toy toyDB = _context.toys.Single(t => t.Code == toy.Code);
            ToyDTO newToy = new ToyDTO();

            toyDB.Name = toy.Name;
            toyDB.Description = toy.Description;
            toyDB.Stock = toy.Stock;
            toyDB.StockThreshold = toy.StockThreshold;
            toyDB.Price = toy.Price;
            toyDB.CategoryId = toy.CategoryId;

            _context.SaveChanges();

            newToy.Name = toy.Name;
            newToy.Description = toy.Description;
            newToy.Stock = toy.Stock;
            newToy.StockThreshold = toy.StockThreshold;
            newToy.Price = toy.Price;
            newToy.CategoryId = toy.CategoryId;

            //as we're adding a new price we should update the price_history, with a transaction or trigger
            return newToy;
        }

        public string ChangePhoto(ToyPhotoViewModel toyPhoto)
        {
            var toyExists = _context.toys.SingleOrDefault(t => t.Code == toyPhoto.Code);

            if (toyExists == null)
            {
                return "No encontrado";
            }

            if (toyPhoto.ImageFile != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        // Read the file data from the IFormFile
                        toyPhoto.ImageFile.CopyTo(ms);

                        // Reset the position of the MemoryStream
                        ms.Position = 0;

                        // Save the file bytes to the database
                        toyExists.ToyImg = ms.ToArray();

                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            return "Foto cambiada exitosamente.";
        }

        //public string ChangePhoto(int id, byte[] newPhoto)
        //{
        //    var toyExists = _context.toys.SingleOrDefault(t => t.Code == id);

        //    if (toyExists == null)
        //    {
        //        return "No encontrado";
        //    }

        //    if (newPhoto != null)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            try
        //            {
        //                // Use the Write method instead of CopyTo
        //                ms.Write(newPhoto, 0, newPhoto.Length);

        //                // Save the file bytes to the database
        //                toyExists.ToyImg = ms.ToArray();

        //                _context.SaveChanges();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Error: {ex.Message}");
        //            }
        //        }
        //    }

        //    return "Foto cambiada exitosamente.";
        //}

        public ToyDTO ChangePrice(int id, int newPrice)
        {
            // Retrieve the toy with the specified ID
            var toy = _context.toys.SingleOrDefault(t => t.Code == id);

            if (toy == null)
            {
                // Handle the case where the toy with the specified ID is not found
                return null;
            }

            // Save the current price to the price_history table
            var priceHistory = new PriceHistory
            {
                ToyCode = toy.Code,
                Price = toy.Price,
                ChangeDate = DateTime.UtcNow // Use UTC time for timestamp
            };

            _context.priceHistories.Add(priceHistory);

            // Update the price of the toy
            toy.Price = newPrice;

            // Save changes to the database
            _context.SaveChanges();

            // Map the updated toy to ToyDTO
            var response = _mapper.Map<ToyDTO>(toy);

            return response;
        }

        public void DeleteToy(int id)
        {
            _context.toys.Remove(_context.toys.Single(t => t.Code == id));
            _context.SaveChanges();
        }
        public void SoftDeleteToy(int id)
        {
            Toy toy = _context.toys.Single(t => t.Code == id);
            if (toy.State == true)
            {
                toy.State = false;
            }
            _context.SaveChanges();
        }

        public void RecoverToy(int id)
        {
            Toy toy = _context.toys.Single(t => t.Code == id);
            if (toy.State == false)
            {
                toy.State = true;
            }
            _context.SaveChanges();
        }
    }
}
