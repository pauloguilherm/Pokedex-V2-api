using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Model;
using Pokedex_v2_api.Repository;

namespace Pokedex_v2_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Pokemon>> GetPokemon()
        {
            return await _pokemonRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetPokemonById(long id)
        {
            var pokemon = await _pokemonRepository.GetById(id);
            return pokemon == null ? NotFound("Pokemon not found.") : pokemon;
        }

        [HttpPost] 
        public async Task<ActionResult<Pokemon>> CatchPokemon(Pokemon pokemon)
        {
            var pokemons = await _pokemonRepository.Create(pokemon);
            return pokemons == null ?  BadRequest() : Ok(pokemons);
        }

        [HttpDelete]
        public async Task<ActionResult<Pokemon>> DropPokemon(long id)
        {
            var pokemonForDrop = await _pokemonRepository.GetById(id);
            return pokemonForDrop == null ? NotFound("Pokemon not found") : Ok(pokemonForDrop);
        }
    }
}
