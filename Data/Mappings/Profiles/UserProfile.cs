using AutoMapper;
using Data.DTOs;
using Data.Models;

namespace Data.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<users, UserDTO>();

            CreateMap<List<users>, List<UserDTO>>()
                .ConvertUsing(src => src.Select(u => new UserDTO
                {
                    user_id = u.user_id, 
                    name = u.name,
                    email = u.email,
                    password = u.password,
                    role_id = u.role_id,
                    state = u.state,
                }).ToList());
        }
    }
}
