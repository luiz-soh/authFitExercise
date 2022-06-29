using Amazon.DynamoDBv2.DataModel;
using infrastructure.Configuration;
using infrastructure.Repository.Interfaces.Token;
using Microsoft.EntityFrameworkCore;
using models.Dto.Token;
using models.Entities.Logged_user;

namespace infrastructure.Repository.Repositories.Token
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IDynamoDBContext _dynamoDBContext;
        public TokenRepository(IDynamoDBContext dynamoDBContext)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task<bool> AdicionaToken(CachedTokenDTO input)
        {
            try
            {
                var userLogin = new LoggedUser(input.UserId, input.Token);
                await _dynamoDBContext.SaveAsync(userLogin);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}