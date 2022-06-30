using models.Dto.Login;
using models.Dto.Token;
using models.Dto.User;

namespace infrastructure.Repository.Interfaces.User
{
    public interface IUserRepository
    {
        Task<int> SignIn(LoginDto input);
        Task SignUp(LoginDto input);
        Task UpdateRefreshToken(TokenDTO input);
        Task<UserDto> GetByRefreshToken(string refreshToken);
    }
}