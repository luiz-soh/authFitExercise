using application.Interfaces.Authentication;
using Models.Dto.Login;
using Models.Dto.Token;
using infrastructure.Repository.Interfaces.User;
using infrastructure.Repository.Interfaces.Token;
using Models.Dto.Login.Register;
using Models.Dto.Error;
using Models.Dto.User;
using Application.Interfaces.User;
using System.Security.Cryptography;

namespace application.Services.Authentication
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationService _authenticationService;


        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _authenticationService = authenticationService;
        }

        public async Task AddUserEmail(AddUserEmailInput input)
        {
            await _userRepository.AddUserEmail(input.Email, input.UserId);
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            return await _userRepository.GetUserData(userId);
        }

        public async Task<TokenDTO> SignIn(LoginInput input)
        {
            var loginDto = new LoginDto(input.Username, input.Password);

            var encryptedPassword = _authenticationService.EncryptPassword(input.Password);
            loginDto.Password = encryptedPassword;

            var user = await _userRepository.SignIn(loginDto);

            if (user.Id != 0)
            {
                var token = GenerateToken(user);

                var cachedToken = new CachedTokenDTO(user.Id, token.UserToken);
                var saveToken = await _tokenRepository.AddUserToken(cachedToken);
                if (!saveToken)
                    return new TokenDTO();

                await _userRepository.UpdateRefreshToken(token);

                return token;
            }
            return new TokenDTO();
        }

        public async Task<ErrorOutput?> SignUp(SignUpInput input)
        {

            var encryptedPassword = _authenticationService.EncryptPassword(input.Password);

            if (await _userRepository.UserAlreadyExists(input.Username))
            {
                return new ErrorOutput("Username ou e-mail ja cadastrado");
            }

            var signUp = new SignUpDto(input, encryptedPassword);
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
                var saveToken = await _tokenRepository.AddUserToken(cachedToken);
                if (!saveToken)
                    return new TokenDTO();

                await _userRepository.UpdateRefreshToken(token);

                return token;
            }

            return new TokenDTO();

        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<List<UserDto>> GetUsersByGymId(int gymId)
        {
            return await _userRepository.GetUsersByGymId(gymId);
        }


        #region private methods
        private TokenDTO GenerateToken(UserDto user)
        {
            var token = _authenticationService.GenerateToken(user.Name,"adm", 24);

            return new TokenDTO(token, GenerateRefreshToken(), user.Id);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion
    }
}