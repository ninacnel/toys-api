using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;

namespace Repository
{
    public class StockRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public StockRepository(toystoreContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public ToyDTO AddStock(int toycode)
        {
            var toyDB = _context.toys.Single(t => t.code == toycode);

            toyDB.stock += 50;

            var toyDTO = _mapper.Map<ToyDTO>(toyDB);

            _context.SaveChanges();

            return toyDTO;
        }
    }
}
