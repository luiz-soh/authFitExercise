using infrastructure.Configuration;
using Infrastructure.Repository.Interfaces.Gym;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models.Configuration.ConnectionString;
using Models.Dto.Gym;
using Models.Dto.Gym.Register;
using Models.Entities.Gym;

namespace Infrastructure.Repository.Repositories.Gym
{
    public class GymRepository : IGymRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<ConnectionStrings> _connectionString;

        public GymRepository(IOptions<ConnectionStrings> connectionString)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _connectionString = connectionString;
        }

        public async Task CreateGym(CreateGymDto input)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var gym = new GymEntity(input);
            context.Gyms.Add(gym);
            await context.SaveChangesAsync();
        }

        public async Task<bool> GymAlreadyExists(string login)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);

            return await context.Gyms.AnyAsync(x => x.GymLogin == login);
        }

        public async Task<GymDto> LoginGym(string login, string password)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);

            var gym = await context.Gyms.Where(x => x.GymPassword == password && x.GymLogin == login).Select(x => new GymDto(x)
            ).FirstOrDefaultAsync();

            if (gym != null)
            {
                return gym;
            }
            else
            {
                return new GymDto();
            }

        }
    }
}
