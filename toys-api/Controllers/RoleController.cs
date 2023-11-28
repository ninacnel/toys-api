using Data.DTOs;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "sysadmin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _service;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService service, ILogger<RoleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<RoleDTO>> GetRoles()
        {
            try
            {
                var response = _service.GetRoles();
                if(response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en RoleController, metodo GetRoles: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpGet("/get-role-by-id")]
        public ActionResult<string> GetRoleById(int id)
        {
            try
            {
                var response = _service.GetRoleById(id);
                if(response == null)
                {
                    return NotFound($"No se ha encontrado un rol con id {id}.");
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en RoleController, metodo GetRoleById: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPost]
        public ActionResult<RoleDTO> AddRole(RoleViewModel role)
        {
            try
            {
                var response = _service.AddRole(role);
                if(response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en RoleController, metodo AddRole: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPatch]
        public ActionResult<RoleDTO> ModifyRole(RoleViewModel role)
        {
            try
            {
                var response = _service.ModifyRole(role);
                if( response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en RoleController, metodo ModifyRole: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
