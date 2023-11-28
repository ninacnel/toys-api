using Data.DTOs;
using Data.ViewModels;

namespace Services.IServices
{
    public interface IAuthService
    {
        AuthDTO Authenticate(AuthViewModel credentials);
        string GenerateToken(AuthDTO user);
    }
}
