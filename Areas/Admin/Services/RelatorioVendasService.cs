using Microsoft.EntityFrameworkCore;
using Mozzafiato.Context;
using Mozzafiato.Models;
namespace Mozzafiato.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext context;

        public RelatorioVendasService(AppDbContext _context)
        {
            this.context = _context;
        }
        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) 
        {
            var resultado = from obj in context.Pedidos select obj;
            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);  
            }
            return await resultado.Include(l=> l.PedidoItens)
                                  .ThenInclude(l=>l.Lanche)
                                  .OrderByDescending(x=>x.PedidoEnviado)
                                  .ToListAsync(); 
        }
         
    }
}
