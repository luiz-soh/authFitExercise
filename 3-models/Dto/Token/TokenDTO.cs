using Models.Dto.User;

namespace Models.Dto.Token
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
            UserId = 0;
        }

        //caso esteja com credenciais certas porem e-mail n�o verificado
        public TokenDTO(UserDto user)
        {
            UserToken = string.Empty;
            RefreshToken = string.Empty;
            UserId = user.Id;
        }
        #endregion

        public int UserId { get; set; }
        public string UserToken { get; set; }
        public string RefreshToken { get; set; }
    }
}