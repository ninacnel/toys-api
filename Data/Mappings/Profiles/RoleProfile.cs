using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<roles, RoleDTO>();

            CreateMap<RoleDTO, roles>();
        }
    }
}
