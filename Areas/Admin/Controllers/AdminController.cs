using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mozzafiato.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) 
            {
                if (User.IsInRole("Admin"))
                {
                    return View("Index");
                }
                else
                    return View("AccessDenied");
            }
            else
                return View("AccessDenied");

        }
    }
}
