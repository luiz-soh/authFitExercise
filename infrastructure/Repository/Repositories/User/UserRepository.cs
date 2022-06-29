using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using models.Dto.Login;
using models.Dto.Token;
using models.Entities.Fit_user;

namespace infrastructure.Repository.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public UserRepository()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> RealizaLogin(LoginDto input)
        {
            using (var contexto = new ContextBase(_optionsBuilder))
            {
                return await contexto.FitUser.Where(x => x.username == input.Username &&
                x.password == input.Password).Select(x => x.user_id).FirstOrDefaultAsync();
            }
        }

        public async Task CriaLogin(LoginDto input)
        {

            var user = new FitUser(input);
            using (var contexto = new ContextBase(_optionsBuilder))
            {
                await contexto.FitUser.AddAsync(user);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task AtualizaRefreshToken(TokenDTO input)
        {
            using (var contexto = new ContextBase(_optionsBuilder))
            {
                var user = await contexto.FitUser.Where(x => x.user_id == input.User_Id).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.refresh_token = input.Refresh_Token;
                    contexto.FitUser.Update(user);
                    await contexto.SaveChangesAsync();
                }
            }
        }
    }
}