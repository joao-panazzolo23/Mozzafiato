using Microsoft.AspNetCore.Mvc;

namespace Mozzafiato.Controllers
{
    public class TesteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult demo()
        {
            return View();
        }
    }
}
