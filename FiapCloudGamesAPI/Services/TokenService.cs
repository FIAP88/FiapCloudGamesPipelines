using FiapCloudGamesAPI.Models;
using FiapCloudGamesAPI.Models.Configuration;
using FiapCloudGamesAPI.Services.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutenticacaoEAutorizacaoCorreto.Services
{
    public class TokenService : ITokenService
    {
        private readonly ConfigSecret _configSecret;
        public TokenService(IOptions<ConfigSecret> configSecret)
        {
            _configSecret = configSecret.Value;
        }

        public string GerarToken(Usuario user )
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configSecret.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Nome),
                    new Claim(ClaimTypes.Role, user.PerfilId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
