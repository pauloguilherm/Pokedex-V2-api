using Microsoft.EntityFrameworkCore;
using Pokedex_v2_api.Model;

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
            var pokemons =  await _context.Pokemon.ToListAsync();
            return pokemons;
        }
        public async Task<Pokemon> GetById(long id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            return pokemon;
        }

        public async Task<Pokemon> Create(Pokemon pokemon)
        {
            _context.Pokemon.Add(pokemon);
            await _context.SaveChangesAsync();

            return pokemon;
        }
        public async Task<Pokemon> Update(Pokemon pokemon)
        {
            _context.Entry(pokemon).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(long id)
        {
            var pokemonDelete = await _context.Pokemon.FindAsync(id);
            _context.Pokemon.Remove(pokemonDelete);
            await _context.SaveChangesAsync();
        }
    }
}
