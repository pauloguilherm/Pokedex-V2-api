using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Models;
using Pokedex_v2_api.Repository;

namespace Pokedex_v2_api.Controllers
{
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [Route("GetFavorites/{id?}")] 
        public async Task<ActionResult<dynamic>> GetPokemons(long id)
        {
            return await _pokemonRepository.GetAll(id);
        }

        [HttpPost]
        [Route("AddFavorite")]
        [Authorize]
        public async Task<ActionResult<dynamic>> CatchPokemon([FromBody] Pokemon pokemon)
        {
            var pokemons = await _pokemonRepository.Create(pokemon);
            if(pokemons == null)
            {
                return new { success = false, message = "error catching pokemon" };
            }
            return new { success = true, message = "Pokemon captured" };
        }

        [HttpDelete]
        [Route("DeleteFavorite/{id?}")]
        public async Task<dynamic> DropPokemon(long id)
        {
            return _pokemonRepository.Delete(id);
        }
    }
}
