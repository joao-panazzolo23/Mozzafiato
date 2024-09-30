using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Models;
using Mozzafiato.Repositories.Interfaces;
using Mozzafiato.ViewModels;
using System.Diagnostics;

namespace Mozzafiato.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly InterLancheRepository _lancheRepository;

        public HomeController(InterLancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }
        public IActionResult Index()
        {
            var HomeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos
            };
            return View(HomeViewModel);
        } 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
