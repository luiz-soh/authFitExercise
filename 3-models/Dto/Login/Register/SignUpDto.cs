using Models.Enums;

namespace Models.Dto.Login.Register
{
    public class SignUpDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserProfileEnum UserProfile { get; set; }


        #region construtores
        public SignUpDto()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserProfile = UserProfileEnum.user;

        }

        public SignUpDto(SignUpInput input, string encryptedPassword)
        {
            Username = input.Username;
            Password = encryptedPassword;
            UserProfile = (UserProfileEnum)input.UserProfile;
        }
        #endregion
    }
}
