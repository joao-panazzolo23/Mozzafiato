using Mozzafiato.Models;

namespace Mozzafiato.Repositories.Interfaces
{
    public interface InterLancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheById(int lancheId); 
    }
}
