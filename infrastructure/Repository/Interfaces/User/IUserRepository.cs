using models.Dto.Login;

namespace infrastructure.Repository.Interfaces.User
{
    public interface IUserRepository
    {
        Task<bool> RealizaLogin(LoginDto input);
        Task CriaLogin(LoginDto input);
    }
}