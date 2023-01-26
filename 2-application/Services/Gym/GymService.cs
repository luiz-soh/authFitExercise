using application.Interfaces.Authentication;
using Application.Interfaces.Gym;
using infrastructure.Repository.Interfaces.Token;
using Infrastructure.Repository.Interfaces.Gym;
using Models.Dto.Error;
using Models.Dto.Gym;
using Models.Dto.Gym.Register;
using Models.Dto.Token;

namespace Application.Services.Gym
{
    public class GymService : IGymService
    {

        private readonly IGymRepository _gymRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationService _authenticationService;

        public GymService(IGymRepository gymRepository, ITokenRepository tokenRepository,
            IAuthenticationService authenticationService)
        {
            _gymRepository = gymRepository;
            _tokenRepository = tokenRepository;
            _authenticationService = authenticationService;
        }

        public async Task<ErrorOutput?> CreateGym(CreateGymInput input)
        {
            var encryptedPassword = _authenticationService.EncryptPassword(input.Password);

            if (await _gymRepository.GymAlreadyExists(input.Login))
            {
                return new ErrorOutput("Este login ja esta sendo utilizado");
            }

            var createGym = new CreateGymDto(input, encryptedPassword);
            await _gymRepository.CreateGym(createGym);
            return null;
        }

        public async Task<GymTokenDto> GetGymToken(GymLoginInput input)
        {
            var encryptedPassword = _authenticationService.EncryptPassword(input.Password);

            var gym = await _gymRepository.LoginGym(input.Login, encryptedPassword);

            if (gym.GymId != 0)
            {
                var token = _authenticationService.GenerateToken(gym.GymName, "gym", 1);

                return new GymTokenDto(gym.GymId, gym.GymName, token);
            }

            return new GymTokenDto();
        }
    }
}
