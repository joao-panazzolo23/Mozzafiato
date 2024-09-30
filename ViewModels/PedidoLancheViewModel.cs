
using Mozzafiato.Models;

namespace Mozzafiato.ViewModels
{
    public class PedidoLancheViewModel
    {
        public Pedido pedido { get; set; } /*esse pedido veio do models e n do migration, 
                                            * só olhar a importação lá de cima*/ 
        public IEnumerable<PedidoDetalhe> pedidoDetalhes { get; set; }
    }
}
