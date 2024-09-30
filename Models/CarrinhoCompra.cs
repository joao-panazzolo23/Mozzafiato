using Microsoft.EntityFrameworkCore;
using Mozzafiato.Context;

namespace Mozzafiato.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessao
            ISession session =
                    services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            // obtem um servico do tipo do contexto
            var context = services.GetService<AppDbContext>();
            //obtem ou gera o id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            //atribui o ID do carrinho na sessao
            session.SetString("CarrinhoId", carrinhoId);
            //retorna o carrinho com o contexto e o id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId &&
            s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                //da pra fazer assim tbm
                //carrinhoCompraItem = new CarrinhoCompraItem();
                //carrinhoCompraItem.CarrinhoCompraId = lanche.LancheId; ...

                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            //isso aq eh uma expressao lambda, posso pesquisar isso melhor depois
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            //dimensiona na memoria a quantidade 
            var quantidadeLocal = 0;

            //se os itens do carrinho de compra não forem nulos
            if (carrinhoCompraItem != null)
            {
                //e se a quantidade de itens for maior do que 1
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                //se não forem maiores do que um
                else
                {      //remova da lista "Carrinho de compras"
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            //salva essas alterações dentro do context, context eh basicamente um mapa pra nossa classe DbContext 
            _context.SaveChanges();
            return quantidadeLocal;
        }
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            //retorna a lista com todos os itens do carrinho
            return CarrinhoCompraItens ?? (CarrinhoCompraItens =
                _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId).Include(x => x.Lanche).ToList());

        }
        public void LimparCarrinho()
        {
            //seleciona todas todos os valores 
            var CarrinhoItens = _context.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            //remove os itens selecionados
            _context.CarrinhoCompraItens.RemoveRange(CarrinhoItens);
            //salva as alterações
            _context.SaveChanges();
        }
        public decimal GetCarrinhoValorTotal()
        { 
            var total = _context.CarrinhoCompraItens
                .Where(c=>c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(C=>C.Lanche.Preco*C.Quantidade)
                .Sum();

            return total;
        }
    }
}
