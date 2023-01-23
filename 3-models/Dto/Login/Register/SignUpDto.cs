using Models.Enums;

namespace Models.Dto.Login.Register
{
    public class SignUpDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? UserEmail { get; set; }
        public UserProfileEnum UserProfile { get; set; }
        public int? GymId { get; set; }


        #region construtores
        public SignUpDto()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserEmail = null;
            UserProfile = UserProfileEnum.user;
            GymId = null;

        }

        public SignUpDto(SignUpInput input, string encryptedPassword)
        {
            Username = input.Username;
            Password = encryptedPassword;
            UserProfile = (UserProfileEnum)input.UserProfile;
            UserEmail = input.UserEmail;
            GymId= input.GymId;
        }
        #endregion
    }
}
