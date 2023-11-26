using Data.DTOs;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Services;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryService> _logger;

        public CategoryController(ICategoryService service, ILogger<CategoryService> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<CategoryDTO>> GetCategories()
        {
            try
            {
                var response = _service.GetCategories();
                if(response == null)
                {
                    return NotFound("No se han encontrado categorias existentes.");
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en CategoryController, metodo GetCategories: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpGet("/get-category-by-id")]
        public ActionResult<CategoryDTO> GetCategoryById(int id)
        {
            try
            {
                var response = _service.GetCategoryById(id);
                if(response == null)
                {
                    return NotFound("No se ha encontrado una catregoría con ese codigo.");
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en CategoryController, metodo GetCategoryById: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPost]
        public ActionResult<CategoryDTO> AddCategory(CategoryViewModel category)
        {
            try
            {
                var response = _service.AddCategory(category);
                if(response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en CategoryController, metodo AddCategory: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpPatch]
        public ActionResult<CategoryDTO> UpdateCategory(CategoryViewModel category)
        {
            try
            {
                var response = _service.UpdateCategory(category);
                if (response == null)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en CategoryController, metodo UpdateCategory: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }

        [HttpDelete]
        public ActionResult<CategoryDTO> DeleteCategory(int id) 
        {
            try
            {
                _service.DeleteCategory(id);

                return Ok();
            }
            catch (Exception exe)
            {
                _logger.LogError($"Error en CategoryController, metodo DeleteCategory: {exe.Message}");
                return BadRequest(exe.Message);
            }
        }
    }
}
