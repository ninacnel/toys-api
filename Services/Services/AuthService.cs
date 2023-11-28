using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _repository;

        public AuthService(AuthRepository repository)
        {
            _repository = repository;
        }

        public AuthDTO Authenticate(AuthViewModel credentials)
        {
            return _repository.Authenticate(credentials);
        }

        public string GenerateToken(AuthDTO user)
        {
            return _repository.GenerateToken(user);
        }
    }
}
