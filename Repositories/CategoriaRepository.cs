using Mozzafiato.Context;
using Mozzafiato.Models;
using Mozzafiato.Repositories.Interfaces;

namespace Mozzafiato.Repositories
{
    public class CategoriaRepository : InterCategoriaRepository
    {
        private readonly AppDbContext _context;
        
        public CategoriaRepository(AppDbContext context) 
        { 
        _context = context;
        }
        public IEnumerable<Categoria> Categorias => _context.Categorias;


    }
}