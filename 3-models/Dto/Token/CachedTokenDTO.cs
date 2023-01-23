namespace Models.Dto.Token
{
    public class CachedTokenDTO
    {
        public CachedTokenDTO(int userId, string token)
        {
            Id = userId;
            Token = token;
        }

        public int Id { get; set; }
        public string Token { get; set; }
    }
}