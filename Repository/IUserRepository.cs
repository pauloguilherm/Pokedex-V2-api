using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public interface IUserRepository
    {
        dynamic GetUser(User user);
        Task<dynamic> CreateUser(User user);
        Task<User> GetUserById(long id);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(long id);
    }
}
