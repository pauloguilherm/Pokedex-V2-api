using Microsoft.IdentityModel.Tokens;
using Pokedex_v2_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pokedex_v2_api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configurationRoot;
        public TokenService(IConfiguration configs)
        {
            _configurationRoot = configs;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var Secret = _configurationRoot["Secret:Key"];
            var Key = Encoding.ASCII.GetBytes(Secret);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Key), 
                        SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
