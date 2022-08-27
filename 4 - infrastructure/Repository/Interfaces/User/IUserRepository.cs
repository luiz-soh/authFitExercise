using models.Dto.Login;
using models.Dto.Token;
using models.Dto.User;
using Models.Dto.Login.Register;

namespace infrastructure.Repository.Interfaces.User
{
    public interface IUserRepository
    {
        Task<int> SignIn(LoginDto input);
        Task SignUp(SignUpDto input);
        Task UpdateRefreshToken(TokenDTO input);
        Task<UserDto> GetByRefreshToken(string refreshToken);
    }
}