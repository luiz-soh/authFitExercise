namespace models.Dto.Token
{
    public class TokenDTO
    {
        #region  construtores
        public TokenDTO(string userToken, string refreshToken, int userId)
        {
            UserToken = userToken;
            RefreshToken = refreshToken;
            UserId = userId;
        }

        public TokenDTO()
        {
            UserToken = string.Empty;
            RefreshToken = string.Empty;
        }
        #endregion

        public int UserId { get; set; }
        public string UserToken { get; set; }
        public string RefreshToken { get; set; }
    }
}