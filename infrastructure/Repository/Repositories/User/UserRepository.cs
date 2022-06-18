using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using models.Dto.Login;

namespace infrastructure.Repository.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public UserRepository()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<bool> RealizaLogin(LoginDto input)
        {
            using (var contexto = new ContextBase(_optionsBuilder))
            {
                return await contexto.FitUser.Where(x => x.username == input.Username &&
                x.password == input.Password).AnyAsync();
            }
        }
    }
}