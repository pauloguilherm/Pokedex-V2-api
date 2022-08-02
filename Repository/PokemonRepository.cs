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
        public async Task<dynamic> GetAll(long id)
        {
            var pokemons = _context.favorites.Select(x => new {x.Id, x.Name }).Where(x => x.Id == id);


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
            try
            {
                _context.favorites.Add(pokemon);
                await _context.SaveChangesAsync();
                return pokemon;
            }
            catch
            {
                return null;
            }
           
        }
        public async Task<dynamic> Delete(long id)
        {
            var pokemonsForDeleted = _context.favorites.Select(x => x).Where(x => x.Id == id);
            try
            {
                foreach (var pokemon in pokemonsForDeleted)
                {
                    _context.favorites.Remove(pokemon);
                };

                await _context.SaveChangesAsync();
                return new { success = true };
            }
            catch
            {
                return new { success = false };
            }
            
        }
    }
}
