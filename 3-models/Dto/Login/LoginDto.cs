using Models.Enums;

namespace Models.Dto.Login
{
    public class LoginDto
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public UserProfileEnum UserProfile { get; set; }

        #region construtores
        public LoginDto()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserProfile = UserProfileEnum.user;

        }

        public LoginDto(string username, string password, int userProfile)
        {
            Username = username;
            Password = password;
            UserProfile = (UserProfileEnum)userProfile;
        }

        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;
            UserProfile = UserProfileEnum.user;
        }
        #endregion
    }
}