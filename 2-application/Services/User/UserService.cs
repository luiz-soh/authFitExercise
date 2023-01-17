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
using Application.Interfaces.User;
using Models.Dto.User;

namespace application.Services.Authentication
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserEmail(AddUserEmailInput input)
        {
            await _userRepository.AddUserEmail(input.Email, input.UserId);
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            return await _userRepository.GetUserData(userId);
        }
    }
}