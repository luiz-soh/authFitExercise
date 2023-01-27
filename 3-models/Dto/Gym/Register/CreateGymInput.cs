using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Gym.Register
{
    public class CreateGymInput
    {
        public CreateGymInput()
        {
            GymName = string.Empty;
            PlanId = 0;
            Password = string.Empty;
            Login = string.Empty;
            Email = string.Empty;
            ConfirmPassword = string.Empty;
        }

        [Required]
        public string GymName { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Za-z0-9])(?=.*?[!@#\\$&*~]).{8,}$", ErrorMessage = "Deve ter pelo menos 8 caracteres e 1 caractere especial")]
        public string Password { get; set; }

        [Required]
        [Compare(otherProperty: "Password", ErrorMessage = "Senhas não batem")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Login { get; set;}

        [Required]
        [EmailAddress]
        public string Email { get;set; }
    }
}
