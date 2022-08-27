using Amazon.DynamoDBv2.DataModel;
using infrastructure.Repository.Interfaces.Token;
using models.Dto.Token;
using models.Entities.LoggedUser;

namespace infrastructure.Repository.Repositories.Token
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IDynamoDBContext _dynamoDBContext;
        public TokenRepository(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task<bool> AddToken(CachedTokenDTO input)
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