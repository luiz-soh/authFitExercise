using Models.Dto.Gym;
using Models.Dto.Gym.Register;

namespace Infrastructure.Repository.Interfaces.Gym
{
    public interface IGymRepository
    {
        Task CreateGym(CreateGymDto input);
        Task<GymDto> LoginGym(string login, string password);
        Task<bool> GymAlreadyExists(string login);
    }
}
