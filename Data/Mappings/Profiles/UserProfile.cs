using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<List<User>, List<UserDTO>>()
                .ConvertUsing(src => src.Select(u => new UserDTO
                {
                    UserId = u.UserId, 
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    RoleId = u.RoleId,
                    State = u.State,
                }).ToList());
        }
    }
}
