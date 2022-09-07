using application.Interfaces.Authentication;
using models.Dto.Login;
using models.Dto.Token;
using infrastructure.Repository.Interfaces.User;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using infrastructure.Repository.Interfaces.Token;
using Microsoft.Extensions.Options;
using models.Configuration.TokenConfiguration;
using Models.Dto.Login.Register;
using Models.Dto.Error;
using models.Dto.User;

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

        public async Task<TokenDTO> SignIn(LoginInput input)
        {
            var loginDto = new LoginDto(input.Username, input.Password);

            var encryptedPassword = Encrypt(input.Password);
            loginDto.Password = encryptedPassword;

            var user = await _userRepository.SignIn(loginDto);

            if (user.Id != 0)
            {
                var token = GenerateToken(user);

                var cachedToken = new CachedTokenDTO(user.Id, token.UserToken);
                var saveToken = await _tokenRepository.AddToken(cachedToken);
                if (!saveToken)
                    return new TokenDTO();

                await _userRepository.UpdateRefreshToken(token);

                return token;
            }
            return new TokenDTO();
        }

        public async Task<ErrorOutput?> SignUp(SignUpInput input)
        {

            var encryptedPassword = Encrypt(input.Password);

            var signUp = new SignUpDto(input, encryptedPassword);

            if (await _userRepository.UserAlreadyExists(input.Username))
            {
                return new ErrorOutput("Username ou e-mail ja cadastrado");
            }

            await _userRepository.SignUp(signUp);
            return null;

            //Mandar e-mail via SES quando produto estiver finalizado

        }

        public async Task<TokenDTO> UpdateToken(UpdateTokenInput input)
        {

            var user = await _userRepository.GetToRefreshToken(input.RefreshToken, input.UserId);

            if (user.Id != 0)
            {
                var token = GenerateToken(user);

                var cachedToken = new CachedTokenDTO(user.Id, token.UserToken);
                var saveToken = await _tokenRepository.AddToken(cachedToken);
                if (!saveToken)
                    return new TokenDTO();

                await _userRepository.UpdateRefreshToken(token);

                return token;
            }

            return new TokenDTO();

        }

        #region private methods

        private TokenDTO GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            string secret = _settings.ClientSecret;
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "Adm")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDTO(tokenHandler.WriteToken(token), GenerateRefreshToken(), user.Id);
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

        #endregion
    }
}