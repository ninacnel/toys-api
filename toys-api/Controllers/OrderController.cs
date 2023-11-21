using Microsoft.AspNetCore.Mvc;

namespace toys_api.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
