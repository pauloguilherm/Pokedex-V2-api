using Microsoft.EntityFrameworkCore;

namespace Pokedex_v2_api.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> user { get; set; }
    }
}
