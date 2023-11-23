using Data.DTOs;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : Controller
    {
        private readonly IToyService _service;
        private readonly ILogger<ToyService> _logger;

        public ToyController(IToyService service, ILogger<ToyService> logger)
        {
            _service = service;
            _logger = logger;
        }
    
        [HttpGet]
        public ActionResult<List<ToyDTO>> GetToys()
        {
            try
            {
                var response = _service.GetToys();
                if(response == null)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo GetToys: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPost]
        public ActionResult<ToyDTO> AddToy([FromBody] ToyViewModel toy)
        {
            try
            {
                var response = _service.AddToy(toy);
                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo AddToy: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
