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

        //[HttpPost("Authenticate")]
        //public ActionResult<string> Autenticar(CredentialsViewModel credentialsViewModel)
        //{
        //    //Paso 1: Validamos las credenciales
        //    credentialsViewModel.Userpassword = credentialsViewModel.Userpassword.GetSHA256();

        //    var user = ValidateCredentials(credentialsViewModel); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

        //    if (user is null) //Si el la función de arriba no devuelve nada es porque los datos son incorrectos, por lo que devolvemos un Unauthorized (un status code 401).
        //        return Unauthorized();

        //    //Paso 2: Crear el token
        //    var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

        //    var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

        //    // validamos el rol de usuario antes encontrado
        //    var role = _authenticationService.ValidateRole(user.Role);

        //    //Los claims son datos en clave->valor que nos permite guardar data del usuario.
        //    var claimsForToken = new List<Claim>();
        //    claimsForToken.Add(new Claim("sub", user.IdUser.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
        //    claimsForToken.Add(new Claim("given_name", user.Username)); //Lo mismo para given_name y family_name, son las convenciones para nombre y apellido. Ustedes pueden usar lo que quieran, pero si alguien que no conoce la app
        //    claimsForToken.Add(new Claim("email", user.Email)); //quiere usar la API por lo general lo que espera es que se estén usando estas keys.
        //    claimsForToken.Add(new Claim("role", role)); //Debería venir del usuario

        //    var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
        //      _config["Authentication:Issuer"],
        //      _config["Authentication:Audience"],
        //      claimsForToken,
        //      DateTime.UtcNow,
        //      DateTime.UtcNow.AddHours(1),
        //      credentials);

        //    var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
        //        .WriteToken(jwtSecurityToken);

        //    return Ok(tokenToReturn);
        //}

        //private Users? ValidateCredentials(CredentialsViewModel credentialsViewModel)
        //{
        //    return _authenticationService.ValidateUser(credentialsViewModel);
        //}
    }
}
