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
        private readonly RoleRepository _roleRepository;

        public AuthRepository(toystoreContext context, IConfiguration config, RoleRepository roleRepository)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _config = config;
            _roleRepository = roleRepository;
        }

        public AuthDTO Authenticate(AuthViewModel credentials)
        {
            var userDB = _context.users.FirstOrDefault(u => u.email == credentials.email);

            if(userDB != null)
            {
                return new AuthDTO
                {
                    user_id = userDB.user_id,
                    name = userDB.name,
                    email = userDB.email,
                    role_id = userDB.role_id,
                };
            }

            return null;
        }

        public string GenerateToken(AuthDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Authentication:SecretForKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userRole = _context.roles.Single(r => r.role_id == user.role_id).role_name;

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
