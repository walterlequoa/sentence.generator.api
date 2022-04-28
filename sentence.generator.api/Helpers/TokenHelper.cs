using Microsoft.IdentityModel.Tokens;
using sentence.generator.api.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace sentence.generator.api.Helpers
{
    public class TokenHelper
    {
        private readonly AppConfiguration configuration;

        public TokenHelper(AppConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> GenerateAccessToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(configuration.SecretKey);

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, userId)
            });

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = configuration.Issuer,
                Audience = configuration.Audience,
                Expires = DateTime.Now.AddMinutes(configuration.ExpiryTimeInMinutes),
                SigningCredentials = signingCredentials,

            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return await Task.Run(() => tokenHandler.WriteToken(securityToken));
        }

        public async Task<string> GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            await System.Threading.Tasks.Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));

            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;
        }
    }
}
