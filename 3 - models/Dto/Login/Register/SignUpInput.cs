using Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Login.Register
{
    public class SignUpInput
    {
        public SignUpInput()
        {
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            UserProfile = (int)UserProfileEnum.user;
            UserEmail = string.Empty;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Deve ter pelo menos 8 caracteres" )]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: "Password", ErrorMessage = "Senhas não batem")]
        public string ConfirmPassword { get; set; }

        public int UserProfile { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }
}
