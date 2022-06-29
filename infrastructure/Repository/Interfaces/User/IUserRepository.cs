using models.Dto.Login;
using models.Dto.Token;

namespace infrastructure.Repository.Interfaces.User
{
    public interface IUserRepository
    {
        Task<int> RealizaLogin(LoginDto input);
        Task CriaLogin(LoginDto input);
        Task AtualizaRefreshToken(TokenDTO input);
    }
}