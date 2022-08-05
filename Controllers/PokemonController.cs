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


        public async Task<ActionResult<dynamic>> DropPokemon([FromBody] Pokemon pokemon)
        {
            var pokemonForDelete =  await _pokemonRepository.Delete(pokemon);
            if(pokemonForDelete == null)
            {
                return BadRequest();
            }
            return Ok(new { success = true, data = pokemon });
        }
    }
}
