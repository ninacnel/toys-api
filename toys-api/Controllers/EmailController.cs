using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace toys_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public ActionResult<string> SendEmail(EmailDto request)
        {
            var response = _emailService.SendEmail(request);
            return Ok(response);
        }
    }
}
