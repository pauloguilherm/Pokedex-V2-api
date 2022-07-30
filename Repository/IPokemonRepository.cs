using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<Pokemon>> GetAll();
        Task<Pokemon> GetById(long id);
        Task<Pokemon> Create(Pokemon pokemon);
        Task<Pokemon>  Delete(long id);
    }
}
