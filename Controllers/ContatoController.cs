using Microsoft.AspNetCore.Mvc;

namespace Mozzafiato.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) //se estiver autenticado permite exibir a view de contato
            {
                return View(); 
            }
            else 
            {//se n estiver, retorna à view de acesso negado
                return RedirectToAction("AccessDenied", "Account");
            };

        }

    }
}
