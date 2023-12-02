using api.Controllers;
using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository;
using Services.IServices;
using Services.Services;
using Xunit;

namespace TestingToyStore
{
    public class ToysTest
    {
        private readonly ToyController _toyController;
        private readonly IToyService _toyService;
        private readonly ToyRepository _toyRepository;
        private readonly ILogger<ToyController> _logger;

        public ToysTest()
        {
            _logger = new Logger<ToyController>(new LoggerFactory());
            _toyService = new ToyService(_toyRepository);
            _toyController = new ToyController(_toyService, _logger);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _toyController.GetToys();
            Assert.IsType<ActionResult<List<ToyDTO>>> (result);
        }

        [Fact]
        public void GetToyById() 
        {
            var id = 123;
            var result = _toyController.GetToyById(id);
            Assert.IsType<ActionResult<ToyDTO>> (result);
        }

        [Fact]
        public void GetToyByIdNotFound()
        {
            var id = 99;

            // Act
            var result = _toyController.GetToyById(id);

            // Assert
            Assert.Null(result);
        }
    }
}
