using Data.DTOs;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService service, ILogger<OrderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<OrderDTO>> GetOrders()
        {
            try
            {
                var response = _service.GetOrders();
                if (response == null || response.Count == 0)
                {
                    return NotFound("No se han registrado ordenes aún.");
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en OrderController, metodo GetOrders: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
        [HttpGet("/get-order-by-id")]
        public ActionResult<OrderDTO> GetOrderById(int id)
        {
            try
            {
                var response = _service.GetOrderById(id);
                if (response == null)
                {
                    return NotFound("No se ha encontrado una orden con ese código.");
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en OrderController, metodo GetOrderById: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPost]
        public ActionResult<OrderDTO> AddOrder(OrderViewModel order)
        {
            try
            {
                var response = _service.AddOrder(order);
                if(response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en OrderController, metodo AddOrder: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
