using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace models.Dto.Login
{
    public class LoginInput
    {
        public LoginInput()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserProfile = (int)UserProfileEnum.user;

        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserProfile { get; set; } = (int)UserProfileEnum.user;
    }
}