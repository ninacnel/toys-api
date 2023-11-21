﻿using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        UserDTO AddUser(UserViewModel user);
        void DeleteUser(int id);
        void RecoverUser(int id);
    }
}
