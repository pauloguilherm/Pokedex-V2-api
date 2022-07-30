using Microsoft.EntityFrameworkCore;
using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        public readonly PokemonContext _context;
        public PokemonRepository(PokemonContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pokemon>> GetAll()
        {
            var pokemons =  await _context.favorites.ToListAsync();
            return pokemons;
        }
        public async Task<Pokemon> GetById(long id)
        {
            var pokemon = await _context.favorites.FindAsync(id);
            if(pokemon == null) return null;
            return pokemon;
        }

        public async Task<Pokemon> Create(Pokemon pokemon)
        {
            _context.favorites.Add(pokemon);
            await _context.SaveChangesAsync();

            return pokemon;
        }
        public async Task<Pokemon> Delete(long id)
        {
            var pokemonDelete = await _context.favorites.FindAsync(id);

            if(pokemonDelete == null)
            {
                return null;
            }
            _context.favorites.Remove(pokemonDelete);
            await _context.SaveChangesAsync();
            return pokemonDelete;
        }
    }
}
