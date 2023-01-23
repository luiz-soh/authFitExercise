using System;
using Models.Dto.Error;
using Models.Dto.Login.Register;
using Models.Dto.Login;
using Models.Dto.Token;
using Models.Dto.User;

namespace Application.Interfaces.User
{
	public interface IUserService
	{
		Task AddUserEmail(AddUserEmailInput input);
		Task<UserDto> GetUserData(int userId);

        Task<TokenDTO> SignIn(LoginInput input);
        Task<ErrorOutput?> SignUp(SignUpInput input);

        Task<TokenDTO> UpdateToken(UpdateTokenInput input);
        Task<bool> DeleteUser(int userId);
    }
}

