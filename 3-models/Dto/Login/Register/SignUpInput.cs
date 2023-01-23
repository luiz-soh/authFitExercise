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
            UserEmail = null;
            GymId = null;
        }

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Za-z0-9])(?=.*?[!@#\\$&*~]).{8,}$", ErrorMessage = "Deve ter pelo menos 8 caracteres e 1 caractere especial")]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: "Password", ErrorMessage = "Senhas não batem")]
        public string ConfirmPassword { get; set; }

        public int UserProfile { get; set; }

        [EmailAddress]
        public string? UserEmail { get; set; }

        public int? GymId { get; set; }
    }
}
