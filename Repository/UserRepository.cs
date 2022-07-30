using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Models;

namespace Pokedex_v2_api.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly UserContext _context;
        public UserRepository (UserContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            var existUser = _context.user.Where(u => u.Name == user.Name || u.Email == user.Email).FirstOrDefault();
            if(existUser != null)
            {
                return null;
            }
            await _context.user.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUser(User user)
        {
            var userName = _context.user.Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefault();
            if (userName == null)
            {
                return null;
            }
            return user;
        }
        public async Task<User> GetUserById(long id)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null) return null;
            return user;
        }
        public async Task<User> DeleteUser(long id)
        {
            var userDelete = await _context.user.FindAsync(id);

            if (userDelete == null)
            {
                return null;
            }
            _context.user.Remove(userDelete);
            await _context.SaveChangesAsync();
            return userDelete;
        }


        public async Task<User> UpdateUser(User user)
        {
            var userUpdate = await _context.user.FindAsync(user.Id);
            if (userUpdate == null) return null;
            _context.user.Update(user);
            return user;
        }
    }
}
