using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Models;
using Pokedex_v2_api.Repository;

namespace Pokedex_v2_api.Controllers
{
    [Route("apipokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        [Route("GetFavorites")]
        [Authorize]
        public async Task<IEnumerable<Pokemon>> GetPokemon()
        {
            return await _pokemonRepository.GetAll();
        }

        [HttpGet("{id}")]
        [Route("GetFavorite")]
        [Authorize]
        public async Task<ActionResult<Pokemon>> GetPokemonById(long id)
        {
            var pokemon = await _pokemonRepository.GetById(id);
            return pokemon == null ? NotFound("Pokemon not found.") : pokemon;
        }

        [HttpPost]
        [Route("Favorite")]
        [Authorize]
        public async Task<ActionResult<Pokemon>> CatchPokemon(Pokemon pokemon)
        {
            var pokemons = await _pokemonRepository.Create(pokemon);
            return pokemons == null ?  BadRequest() : Ok(pokemons);
        }

        [HttpDelete("{id}")]
        [Route("DeleteFavorite")]
        [Authorize]
        public async Task<ActionResult<Pokemon>> DropPokemon(long id)
        {
            var pokemonForDrop = await _pokemonRepository.GetById(id);
            if (pokemonForDrop == null)
            {
                return NotFound("Pokemon not found");
            };
            return await _pokemonRepository.Delete(pokemonForDrop.Id);
        }
    }
}
