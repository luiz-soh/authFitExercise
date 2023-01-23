using System.ComponentModel.DataAnnotations;

namespace Models.Dto.Gym
{
    public class GymLoginInput
    {
        public GymLoginInput()
        {
            Login = string.Empty;
            Password= string.Empty;
        }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
