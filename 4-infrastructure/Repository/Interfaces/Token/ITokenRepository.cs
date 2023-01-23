using Models.Dto.Token;

namespace infrastructure.Repository.Interfaces.Token
{
    public interface ITokenRepository
    {
        Task<bool> AddUserToken(CachedTokenDTO input);
        Task<bool> AddGymToken(CachedTokenDTO input);
    }
}