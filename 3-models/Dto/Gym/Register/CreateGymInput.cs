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
        }

        [Required]
        public string GymName { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Login { get; set;}

        [Required]
        [EmailAddress]
        public string Email { get;set; }
    }
}
