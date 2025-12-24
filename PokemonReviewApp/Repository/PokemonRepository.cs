using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository // ინტერფეისი გავხადეთ მის მშობლად
    {
        private readonly DataContext _context;  // readonly-თი ერთხელ ანიჭებ ცვლადს მნიშვნელობას და შემდეგ ვეღარ ცვლი
                                                // ამას ვანიჭებთ Database-ის მონაცემებს
        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList();
        }
    }
}
