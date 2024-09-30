using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Repositories.Interfaces;

namespace Mozzafiato.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly InterCategoriaRepository _categoriaRepository;

        public CategoriaMenu(InterCategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(c=> c.CategoriaName);
            return View(categorias);
        }
    }
}
