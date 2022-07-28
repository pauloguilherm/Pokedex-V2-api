using Microsoft.EntityFrameworkCore;

namespace Pokedex_v2_api.Model
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pokemon> Pokemon { get; set; }
    }
}
