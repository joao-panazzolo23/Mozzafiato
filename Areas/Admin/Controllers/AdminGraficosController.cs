using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Areas.Admin.Services;


namespace Mozzafiato.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminGraficosController : Controller
    {
        private readonly GraficosVendaService _graficosVendas;
        public AdminGraficosController(GraficosVendaService graficosVenda)
        {
            _graficosVendas = graficosVenda ?? throw new ArgumentException(nameof(graficosVenda));
        }
        public JsonResult VendasLanches(int dias=365)
        {
            var lanchesVendasTotais = _graficosVendas.GetVendasLanches(dias);
            return Json(lanchesVendasTotais);

        }
        [HttpGet]
        public IActionResult VendasSemanal(int dias)
        {
            return View();
        }
        [HttpGet]
        public IActionResult VendasMensal(int dias)
        {
            return View();
        }
        public IActionResult VendasAnuais365() /*index no csproj do macorati*/
        {
            return View();
        }
    }

}
   
       
   




