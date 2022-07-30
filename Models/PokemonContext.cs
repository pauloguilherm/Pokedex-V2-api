using Microsoft.EntityFrameworkCore;

namespace Pokedex_v2_api.Models
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Pokemon> favorites { get; set; }
    }
}
