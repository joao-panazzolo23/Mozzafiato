using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Models;
using Mozzafiato.Repositories.Interfaces;

namespace Mozzafiato.Controllers
{
    public class PedidoController : Controller
    {

        private readonly InterPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(InterPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Checkout()
        { 
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Pedido pedido)  
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            if (_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState.AddModelError("", "Seu Carrinho está vazio!");
            }
            foreach (var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
                
            }
            if (ModelState.IsValid)
            {
                pedido.TotalItensPedido = totalItensPedido;
                pedido.PedidoTotal = precoTotalPedido;
                _pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido! ";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoValorTotal();

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}
