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
                if (response == null)
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

        [HttpGet("/get-toy-by-id")]
        public ActionResult<ToyDTO> GetToyById(int id)
        {
            try
            {
                var response = _service.GetToyById(id);
                if (response == null)
                {
                    return NotFound($"The toy with code {id} does not exist.");
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

        [HttpPut]
        public ActionResult<ToyDTO> UpdateToy(ToyViewModel toy)
        {
            try
            {
                var response = _service.UpdateToy(toy);
                if (response == null)
                {
                    return BadRequest();
                }
                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo UpdateToy: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPatch("/change-price")]
        public ActionResult<ToyDTO> ChangePrice(int id, int newPrice)
        {
            try
            {
                var response = _service.ChangePrice(id, newPrice);
                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo ChangePrice: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpDelete]
        public ActionResult<ToyDTO> DeleteToy(int id)
        {
            try
            {
                _service.DeleteToy(id);

                return Ok();
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo DeleteToy: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
        [HttpPatch("/soft-delete-toy")]
        public ActionResult<ToyDTO> SoftDeleteToy(int id)
        {
            try
            {
                _service.SoftDeleteToy(id);

                return Ok();
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo SoftDeleteToy: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPatch("/recover-toy")]
        public ActionResult<ToyDTO> RecoverToy(int id)
        {
            try
            {
                _service.RecoverToy(id);

                return Ok();
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en ToyController, metodo RecoverToy: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
