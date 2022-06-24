using models.Dto.Login;
using models.Dto.Token;

namespace application.Interfaces.Authentication
{
    public interface IAuthentication
    {
        Task<Token> RealizaLogin(LoginInput input);
        Task CriaLogin(LoginInput input);
    }
}