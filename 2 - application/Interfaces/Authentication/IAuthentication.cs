using models.Dto.Login;
using models.Dto.Token;

namespace application.Interfaces.Authentication
{
    public interface IAuthentication
    {
        Task<TokenDTO> SignIn(LoginInput input);
        Task SignUp(LoginInput input);

        Task<TokenDTO> UpdateToken(string refreshToken);
    }
}