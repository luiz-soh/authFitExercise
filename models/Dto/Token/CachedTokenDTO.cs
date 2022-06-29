namespace models.Dto.Token
{
    public class CachedTokenDTO
    {
        public CachedTokenDTO(int userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
        }

        public int UserId { get; set; }
        public string Token { get; set; }
    }
}