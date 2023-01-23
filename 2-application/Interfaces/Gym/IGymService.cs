using Models.Dto.Error;
using Models.Dto.Gym.Register;
using Models.Dto.Gym;
using Models.Dto.Token;

namespace Application.Interfaces.Gym
{
    public interface IGymService
    {
        Task<GymTokenDto> GetGymToken(GymLoginInput input);
        Task<ErrorOutput?> CreateGym(CreateGymInput input);
    }
}
