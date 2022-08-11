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

        [HttpGet("{id}")]
        [Route("GetFavorites/{id?}")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetPokemons(long id)
        {
            var pokemons = await _pokemonRepository.GetAll(id);
            if(pokemons == null)
            {
                return NotFound(new { success = false, message = "Pokemons not found"});
            }
            return Ok(new { success = true, data = pokemons});
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
            return new { success = true, message = pokemons.Name  + " Captured" };
        }

        [HttpDelete]
        [Route("DeleteFavorite")]
        [Authorize]

        public async Task<ActionResult<dynamic>> DropPokemon([FromBody] Pokemon pokemon)
        {
            var pokemonForDelete =  await _pokemonRepository.Delete(pokemon);
            if(pokemonForDelete == null)
            {
                return BadRequest(new {success = false, message = "Error when deleting pokemon" });
            }
            return Ok(new { success = true, data = pokemon, message = pokemon.Name + " released" });
        }
    }
}
