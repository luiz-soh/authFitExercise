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

        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}