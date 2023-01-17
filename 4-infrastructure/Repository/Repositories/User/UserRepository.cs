using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using models.Dto.Login;
using models.Dto.Token;
using models.Dto.User;
using models.Entities.FitUser;
using Models.Configuration.ConnectionString;
using Models.Dto.Login.Register;

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

        public async Task<UserDto> SignIn(LoginDto input)
        {
            using var contexto = new ContextBase(_optionsBuilder, _connectionString);
            var user = await contexto.FitUser.Where(x => x.Username == input.Username &&
            x.Password == input.Password).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task SignUp(SignUpDto input)
        {

            var user = new FitUser(input);
            using var contexto = new ContextBase(_optionsBuilder, _connectionString);
            await contexto.FitUser.AddAsync(user);
            await contexto.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(TokenDTO input)
        {
            using var contexto = new ContextBase(_optionsBuilder, _connectionString);
            var user = await contexto.FitUser.Where(x => x.UserId == input.UserId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.RefreshToken = input.RefreshToken;
                contexto.FitUser.Update(user);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetToRefreshToken(string refreshToken, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var user = await context.FitUser.Where(x => x.RefreshToken == refreshToken && x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<bool> UserAlreadyExists(string username)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            return await context.FitUser.Where(u => u.Username == username).AnyAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                context.FitUser.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddUserEmail(string email, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.UserEmail = email;
                context.FitUser.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _connectionString);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }
    }
}