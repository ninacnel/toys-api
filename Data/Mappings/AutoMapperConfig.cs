using AutoMapper;
using Data.Mappings.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.AddProfile<UserProfile>();
                cfg.AddProfile<ToyProfile>();
                cfg.AddProfile<PriceProfile>();
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
