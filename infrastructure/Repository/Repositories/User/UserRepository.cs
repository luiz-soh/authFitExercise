using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using models.Dto.Login;
using models.Dto.Token;
using models.Dto.User;
using models.Entities.FitUser;
using Models.Configuration.ConnectionString;

namespace infrastructure.Repository.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<ConnectionStrings> _connectionString;

        public UserRepository(IOptions<ConnectionStrings> connectionString)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _connectionString = connectionString;
        }

        public async Task<int> SignIn(LoginDto input)
        {
            using (var contexto = new ContextBase(_optionsBuilder, _connectionString))
            {
                return await contexto.FitUser.Where(x => x.Username == input.Username &&
                x.Password == input.Password).Select(x => x.UserId).FirstOrDefaultAsync();
            }
        }

        public async Task SignUp(LoginDto input)
        {

            var user = new FitUser(input);
            using (var contexto = new ContextBase(_optionsBuilder, _connectionString))
            {
                await contexto.FitUser.AddAsync(user);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task UpdateRefreshToken(TokenDTO input)
        {
            using (var contexto = new ContextBase(_optionsBuilder, _connectionString))
            {
                var user = await contexto.FitUser.Where(x => x.UserId == input.UserId).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.RefreshToken = input.RefreshToken;
                    contexto.FitUser.Update(user);
                    await contexto.SaveChangesAsync();
                }
            }
        }

        public async Task<UserDto> GetByRefreshToken(string refreshToken)
        {
            using (var context = new ContextBase(_optionsBuilder, _connectionString))
            {
               var user = await context.FitUser.Where(x => x.RefreshToken == refreshToken).FirstOrDefaultAsync();

                if (user != null)
                    return new UserDto(user.UserId, user.Username);
                else
                    return new UserDto();
            }
        }
    }
}