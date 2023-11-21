using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class ToyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
