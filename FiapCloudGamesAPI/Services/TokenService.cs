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

        public string GerarToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configSecret.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Role, user.Perfil.Descricao)
            };

            if (user.Perfil?.PerfilPermissoes != null)
            {
                foreach (var permissao in user.Perfil.PerfilPermissoes)
                {
                    claims.Add(new Claim("permission", permissao.Permissao.Descricao));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public long GetUsuarioId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configSecret.Secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                {
                    return userId;
                }

                throw new SecurityTokenException("Invalid token: User ID not found.");
            }
            catch
            {
                throw new SecurityTokenException("Invalid token.");
            }
        }

    }
}
