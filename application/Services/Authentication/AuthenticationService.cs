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

namespace application.Services.Authentication
{
    public class AuthenticationService : IAuthentication
    {

        private readonly IUserRepository _userRepository;

        private readonly IConfiguration _configuration;


        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

        }

        public async Task<Token> RealizaLogin(LoginInput input)
        {
            var loginDto = new LoginDto(input.Username, input.Password);

            var encryptedPassword = Encrypt(input.Password);

            var exists = await _userRepository.RealizaLogin(loginDto);

            if(exists)
            {
                var token = GenerateToken(input);
                //TODO
                //Salvar o refreshToken no banco
                //Salvar o token no dynamoDB
            }
            return new Token();
        }

        public async Task CriaLogin(LoginInput input){

            var encryptedPassword = Encrypt(input.Password);

            var loginDto = new LoginDto(input.Username, encryptedPassword);

            await _userRepository.CriaLogin(loginDto);

        }

        private Token GenerateToken(LoginInput input)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secret = _configuration["ClientSecret"];
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

            return new Token(tokenHandler.WriteToken(token), GenerateRefreshToken());
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
                var bytes = Encoding.UTF8.GetBytes($"{_configuration["PreSalt"]}{dataToEncrypt}{_configuration["PosSalt"]}");
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