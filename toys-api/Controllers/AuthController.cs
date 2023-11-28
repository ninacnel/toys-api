using api.Controllers;
using Data.DTOs;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<string> Login(UserViewModel user)
        {
            try
            {
                var response = _service.Authenticate(user);
                if (response == null)
                    return Unauthorized();

                var token = _service.GenerateToken(user);
                return Ok(token);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en AuthController, metodo Login: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
