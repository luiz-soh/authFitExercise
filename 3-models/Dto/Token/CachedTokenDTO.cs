namespace models.Dto.Token
{
    public class CachedTokenDTO
    {
        public CachedTokenDTO(int userId, string token)
        {
            UserId = userId;
            Token = token;
        }

        public int UserId { get; set; }
        public string Token { get; set; }
    }
}