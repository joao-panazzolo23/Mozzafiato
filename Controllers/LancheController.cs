using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Models;
using Mozzafiato.Repositories;
using Mozzafiato.Repositories.Interfaces;
using Mozzafiato.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace Mozzafiato.Controllers
{
    public class LancheController : Controller
    {
        private readonly LancheRepository _lancheRepository;

        public LancheController(InterLancheRepository lancheRepository)
        {
            _lancheRepository = (LancheRepository)lancheRepository;
        }
        // GET: LancheController
        public ActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaName
                    .Equals(categoria))
                    .OrderBy(c => c.Nome);
            }
            var lanchesListViewModel = new LancheListViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: LancheController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: LancheController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }
        }

        // GET: LancheController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LancheController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(List));
            }
            catch
            {
                return View();
            }

        }
        public IActionResult Details(int LancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == LancheId);
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }
            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });

        }
    }
}

