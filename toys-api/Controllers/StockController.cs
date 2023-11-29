using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly IStockService _service;
        private readonly ILogger<StockController> _logger;

        public StockController(IStockService service, ILogger<StockController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPatch]
        public ActionResult<ToyDTO> AddStock(int toycode)
        {
            try
            {
                var response = _service.AddStock(toycode);
                if(response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en StockController, metodo AddStock: {exe.Message}");
                return BadRequest();
            }
        }
    }
}
