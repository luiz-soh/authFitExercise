using Amazon.DynamoDBv2.DataModel;

namespace Models.Entities.Gym
{
    public class LoggedGym
    {
        public LoggedGym(int userId, string token)
        {
            GymId = userId;
            Token = token;
            Ttl = DateTimeOffset.Now.AddHours(1).ToUnixTimeSeconds();
        }

        public LoggedGym()
        {
            GymId = 0;
            Token = string.Empty;
            Ttl = DateTimeOffset.Now.AddHours(1).ToUnixTimeSeconds();
        }

        [DynamoDBHashKey("gym_id")]
        public int GymId { get; set; }

        [DynamoDBProperty("token")]
        public string Token { get; set; }

        [DynamoDBProperty("ttl")]
        public long Ttl { get; set; }
    }
}
