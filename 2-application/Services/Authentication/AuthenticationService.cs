using application.Interfaces.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Models.Configuration.TokenConfiguration;
using Infrastructure.Repository.Interfaces.Gym;

namespace application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IGymRepository _gymRepository;
        private readonly TokenConfiguration _settings;


        public AuthenticationService(IOptions<TokenConfiguration> settings,
        IGymRepository gymRepository)
        {
            _settings = settings.Value;
            _gymRepository = gymRepository;
        }

        public string EncryptPassword(string dataToEncrypt)
        {
            string encryptedData;
            var bytes = Encoding.UTF8.GetBytes($"{_settings.PreSalt}{dataToEncrypt}{_settings.PosSalt}");
            var hash = SHA512.HashData(bytes);
            encryptedData = GetStringFromHash(hash);

            return encryptedData;
        }

        public string GenerateToken(string name, string role, int validyHours)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _settings.ClientSecret;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(validyHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        #region private methods
        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }

        #endregion
    }
}