using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public interface IPokemonRepository
    {
        Task<dynamic> GetAll(long id);
        Task<Pokemon> Create(Pokemon pokemon);
        Task<dynamic> Delete(Pokemon pokemon);
    }
}
