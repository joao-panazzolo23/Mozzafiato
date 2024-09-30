using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Models;
using Mozzafiato.Repositories.Interfaces;
using Mozzafiato.ViewModels;


namespace Mozzafiato.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly InterLancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(InterLancheRepository lancheRepository,
                                        CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoValorTotal()
            };

            return View(carrinhoCompraVM);
        }
            public IActionResult AdicionarItemNoCarrinho(int lancheId)
            {
                var lanchesSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

                if (lanchesSelecionado != null)
                {
                    _carrinhoCompra.AdicionarAoCarrinho(lanchesSelecionado);
                }
                return RedirectToAction("Index");
            }
            public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
            {
                var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(p => p.LancheId == lancheId);

                {
                    _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
                }
                return RedirectToAction("Index");
            }
        }
    }
