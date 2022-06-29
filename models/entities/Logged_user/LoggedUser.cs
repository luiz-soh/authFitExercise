using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace models.Entities.Logged_user
{
    [DynamoDBTable("fitexercisetokens")]
    public class LoggedUser
    {
        public LoggedUser(int userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
            this.ttl = DateTimeOffset.Now.AddDays(1).ToUnixTimeSeconds();
        }

        [DynamoDBHashKey("user_id")]
        public int UserId { get; set; }

        [DynamoDBProperty("token")]
        public string Token { get; set; }

        [DynamoDBProperty("ttl")]
        public long ttl { get; set; }
    }
}