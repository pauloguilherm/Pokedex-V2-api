using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(User user);
        Task<User> CreateUser(User user);
        Task<User> GetUserById(long id);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(long id);
    }
}
