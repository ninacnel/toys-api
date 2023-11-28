using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AuthRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthRepository(toystoreContext context, IConfiguration config)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _config = config;
        }

        public UserDTO Authenticate(UserViewModel user)
        {
            var userDB = _context.users.FirstOrDefault(u => u.email == user.email);

            // Simulate database lookup to validate credentials
            if (userDB != null)
            {
                return new UserDTO
                {
                    user_id = user.user_id,
                    name = user.name,
                    email = user.email,
                    password = user.password,
                    role_id = user.role_id,
                    // Include any additional user information needed in the token
                };
            }

            return null; // Invalid credentials
        }

        public string GenerateToken(UserViewModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim("iduser", user.user_id.ToString()),
            new Claim("name", user.name),
            new Claim("email", user.email),
            new Claim("role", user.role_id.ToString())
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
