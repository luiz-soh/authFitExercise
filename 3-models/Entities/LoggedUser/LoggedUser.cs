using Amazon.DynamoDBv2.DataModel;

namespace Models.Entities.LoggedUser
{
    [DynamoDBTable("fitexercisetokens")]
    public class LoggedUser
    {
        public LoggedUser(int userId, string token)
        {
            UserId = userId;
            Token = token;
            Ttl = DateTimeOffset.Now.AddDays(1).ToUnixTimeSeconds();
        }

        public LoggedUser(){
            UserId = 0;
            Token = string.Empty;
            Ttl = DateTimeOffset.Now.AddDays(1).ToUnixTimeSeconds();
        }

        [DynamoDBHashKey("user_id")]
        public int UserId { get; set; }

        [DynamoDBProperty("token")]
        public string Token { get; set; }

        [DynamoDBProperty("ttl")]
        public long Ttl { get; set; }
    }
}