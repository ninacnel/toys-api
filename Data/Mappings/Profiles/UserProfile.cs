using AutoMapper;
using Data.DTOs;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
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
