using models.Dto.User;

namespace models.Dto.Token
{
    public class TokenDTO
    {
        #region  construtores
        public TokenDTO(string userToken, string refreshToken, int userId, bool isEmailVerified)
        {
            UserToken = userToken;
            RefreshToken = refreshToken;
            UserId = userId;
            IsEmailVerified = isEmailVerified;
        }

        public TokenDTO()
        {
            UserToken = string.Empty;
            RefreshToken = string.Empty;
            UserId = 0;
            IsEmailVerified = false;
        }

        //caso esteja com credenciais certas porem e-mail não verificado
        public TokenDTO(UserDto user)
        {
            UserToken = string.Empty;
            RefreshToken = string.Empty;
            UserId = user.Id;
            IsEmailVerified = user.IsEmailVerified;
        }
        #endregion

        public int UserId { get; set; }
        public string UserToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}