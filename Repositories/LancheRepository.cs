using Microsoft.EntityFrameworkCore;
using Mozzafiato.Context;
using Mozzafiato.Models;
using Mozzafiato.Repositories.Interfaces;

namespace Mozzafiato.Repositories
{
    public class LancheRepository : InterLancheRepository 
    {
        private readonly AppDbContext _context;

        #region Construtores
        public LancheRepository(AppDbContext contexto)
        {
           _context = contexto;
        }

        #endregion
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.
                                   Where(l => l.IsLanchePreferido).
                                   Include(c => c.Categoria);
        
        public Lanche GetLancheById(int lancheId)
        { 
        return _context.Lanches.FirstOrDefault(l=>l.LancheId == lancheId);
        }
    }
}
