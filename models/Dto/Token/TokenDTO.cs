namespace models.Dto.Token
{
    public class TokenDTO
    {
        #region  construtores
        public TokenDTO(string user_Token, string refresh_Token, int userId)
        {
            this.User_Token = user_Token;
            this.Refresh_Token = refresh_Token;
            this.User_Id = userId;
        }

        public TokenDTO()
        {
            this.User_Token = string.Empty;
            this.Refresh_Token = string.Empty;
        }
        #endregion

        public int User_Id { get; set; }
        public string User_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}