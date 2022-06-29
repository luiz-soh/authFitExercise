using application.Interfaces.Authentication;
using models.Dto.Login;
using models.Dto.Token;
using infrastructure.Repository.Interfaces.User;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using infrastructure.Repository.Interfaces.Token;
using Microsoft.Extensions.Options;
using models.Configuration.TokenConfiguration;

namespace application.Services.Authentication
{
    public class AuthenticationService : IAuthentication
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly TokenConfiguration _settings;


        public AuthenticationService(IUserRepository userRepository,
        ITokenRepository tokenRepository, IOptions<TokenConfiguration> Settings)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _settings = Settings.Value;
        }

        public async Task<TokenDTO> RealizaLogin(LoginInput input)
        {
            var loginDto = new LoginDto(input.Username, input.Password);

            var encryptedPassword = Encrypt(input.Password);
            loginDto.Password = encryptedPassword;

            var userId = await _userRepository.RealizaLogin(loginDto);

            if (userId != 0)
            {
                var token = GenerateToken(input, userId);

                var cachedToken = new CachedTokenDTO(userId, token.User_Token);
                var saveToken = await _tokenRepository.AdicionaToken(cachedToken);
                if (!saveToken)
                    return new TokenDTO();

                await _userRepository.AtualizaRefreshToken(token);
            }
            return new TokenDTO();
        }

        public async Task CriaLogin(LoginInput input)
        {

            var encryptedPassword = Encrypt(input.Password);

            var loginDto = new LoginDto(input.Username, encryptedPassword);

            await _userRepository.CriaLogin(loginDto);

        }

        private TokenDTO GenerateToken(LoginInput input, int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secret = _settings.ClientSecret;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, input.Username),
                    new Claim(ClaimTypes.Role, "Adm")
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDTO(tokenHandler.WriteToken(token), GenerateRefreshToken(), userId);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string Encrypt(string dataToEncrypt)
        {
            string encryptedData;

            using (var sha512 = SHA512.Create())
            {
                var bytes = Encoding.UTF8.GetBytes($"{_settings.PreSalt}{dataToEncrypt}{_settings.PosSalt}");
                var hash = sha512.ComputeHash(bytes);
                encryptedData = GetStringFromHash(hash);
            }

            return encryptedData;
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }

            return result.ToString();
        }
    }
}