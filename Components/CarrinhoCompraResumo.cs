using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Models;
using Mozzafiato.ViewModels;
namespace Mozzafiato.Components

{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;
     
        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra) 
        {
            _carrinhoCompra = carrinhoCompra;
        }
        public IViewComponentResult Invoke()
        {
            var itens = new List<CarrinhoCompraItem>();

            _carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoValorTotal(),
            };
            return View(carrinhoCompraVM);
        }
    }
}
