using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _repository;

        public AuthService(AuthRepository repository)
        {
            _repository = repository;
        }

        public UserDTO Authenticate(UserViewModel user)
        {
            return _repository.Authenticate(user);
        }

        public string GenerateToken(UserViewModel user)
        {
            return _repository.GenerateToken(user);
        }
    }
}
