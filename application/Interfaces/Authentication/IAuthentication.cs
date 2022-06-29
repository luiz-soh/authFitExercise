using models.Dto.Login;
using models.Dto.Token;

namespace application.Interfaces.Authentication
{
    public interface IAuthentication
    {
        Task<TokenDTO> RealizaLogin(LoginInput input);
        Task CriaLogin(LoginInput input);
    }
}