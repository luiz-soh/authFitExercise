using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.User;
using Models.Dto.Login.Register;

namespace infrastructure.Repository.Interfaces.User
{
    public interface IUserRepository
    {
        Task<UserDto> SignIn(LoginDto input);
        Task SignUp(SignUpDto input);
        Task UpdateRefreshToken(TokenDTO input);
        Task<UserDto> GetToRefreshToken(string refreshToken, int userId);
        Task<bool> UserAlreadyExists(string username);
        Task<bool> DeleteUser(int userId);
        Task AddUserEmail(string email, int userId);
        Task<UserDto> GetUserData(int userId);
        Task<List<UserDto>> GetUsersByGymId(int gymId);
    }
}