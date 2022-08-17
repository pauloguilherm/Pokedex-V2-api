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
        public async Task<dynamic> GetAll(int id)
        {
            var pokemons = _context.favorites.Select(x => x).Where(x => x.CoachId == id);
            if(pokemons == null)
            {
                return null;
            }
            return pokemons;
        }
        public async Task<dynamic> Create(Pokemon pokemon)
        {
            try
                {
                    await _context.favorites.AddAsync(pokemon);
                    await _context.SaveChangesAsync();
                    return pokemon;
                }
            catch
                {
                    return null;
                }
           
        }
        public async Task<dynamic> Delete(Pokemon pokemon)
        {
            var pokemonForDelete = _context.favorites.FirstOrDefault(x => x.CoachId == pokemon.CoachId && x.Name == pokemon.Name);
            if(pokemonForDelete != null)
            {
                try
                {
                    _context.favorites.Remove(pokemonForDelete);
                    await _context.SaveChangesAsync();
                    return new { success = true };
                }
                catch(Exception ex)
                {
                    return new { message = ex.ToString() };
                }
            }
            return null;
        }
    }
}
