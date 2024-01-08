using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();

            CreateMap<RoleDTO, Role>();
        }
    }
}
