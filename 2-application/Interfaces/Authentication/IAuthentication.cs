using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.Error;
using Models.Dto.Login.Register;

namespace application.Interfaces.Authentication
{
    public interface IAuthentication
    {
        Task<TokenDTO> SignIn(LoginInput input);
        Task<ErrorOutput?> SignUp(SignUpInput input);

        Task<TokenDTO> UpdateToken(UpdateTokenInput input);
        Task<bool> DeleteUser(int userId);
    }
}