using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthService
    {
        AuthDTO Authenticate(AuthViewModel credentials);
        string GenerateToken(AuthDTO user);
    }
}
