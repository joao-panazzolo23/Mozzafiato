using Microsoft.AspNetCore.Mvc;
using Mozzafiato.Context;
using Mozzafiato.Models;
namespace Mozzafiato.Areas.Admin.Services
{
    public class GraficosVendaService
    {
        private readonly AppDbContext context;

        public GraficosVendaService(AppDbContext context)
        {
            this.context = context;
        }
        public List<LancheGrafico> GetVendasLanches(int dias = 365)
        {
            var data = DateTime.Now.AddDays(-dias);
            var lanches = (from pd in context.PedidoDetalhes
                           join l in context.Lanches
                           on pd.LancheId equals l.LancheId
                           where pd.Pedido.PedidoEnviado >= data
                           group new { pd, l } by new { pd.LancheId, l.Nome } into g
                           select new
                           {
                               LanchesNome = g.Key.Nome,
                               LancheQuantidade = g.Sum(x => x.pd.Quantidade),
                               LanchesValorTotal = g.Sum(x => x.pd.Preco * x.pd.Quantidade),
                           });
            var lista = new List<LancheGrafico>();

            foreach (var item in lanches)
            {
                var lanche = new LancheGrafico();
                lanche.LancheNome = item.LanchesNome;
                lanche.LancheQuantidade = item.LancheQuantidade;
                lanche.LanchesValorTotal = item.LanchesValorTotal;
                lista.Add(lanche);
            }
            return lista;
        }

    }
}
