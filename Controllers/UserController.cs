using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex_v2_api.Models;
using Pokedex_v2_api.Repository;
using Pokedex_v2_api.Services;

namespace Pokedex_v2_api.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        public UserController(IUserRepository userRepository, IConfiguration Configuration)
        {
            _userRepository = userRepository;
            _configuration = Configuration;
        }


        [HttpPost]
        [Route("signIn")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> SignIn([FromBody] User user)
        {
            var login = _userRepository.GetUser(user);
            if(login.Result == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            };

            var token = new TokenService(_configuration);
            var tokenGenerated = token.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = tokenGenerated,
            };
        }

        [HttpPost]
        [Route("signUp")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> SignUp([FromBody] User user)
        {
            var createdUser = await _userRepository.CreateUser(user);

            if(createdUser == null)
            {
                return NotFound(new {message = "Usuário ou email já cadastrados"});
            }
            var token = new TokenService(_configuration);
            var tokenGenerated = token.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = tokenGenerated,
            };
        }
    }
}
