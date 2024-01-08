using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Data;

namespace Repository
{
    public class AuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthRepository(DataContext context, IConfiguration config)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _config = config;
        }

        public AuthDTO Authenticate(AuthViewModel credentials)
        {
            var userDB = _context.users.FirstOrDefault(u => u.Name == credentials.username);

            if (userDB != null)
            {
                // Verify the password
                if (BCrypt.Net.BCrypt.Verify(credentials.password, userDB.Password))
                {
                    // Password is correct, return AuthDTO
                    return new AuthDTO
                    {
                        user_id = userDB.UserId,
                        name = userDB.Name,
                        email = userDB.Email,
                        role_id = userDB.RoleId,
                    };
                }
            }

            return null;
        }

        public string GenerateToken(AuthDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRole = _context.roles.Single(r => r.RoleId == user.role_id).RoleName;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Role, userRole)
            // Add any additional claims as needed
        };

            var token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
