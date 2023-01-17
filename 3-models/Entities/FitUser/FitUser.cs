using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Dto.Login.Register;
using Models.Enums;

namespace Models.Entities.FitUser
{
    [Table("fit_user")]
    public class FitUser
    {

        #region Contrutores

        public FitUser()
        {
            Username = string.Empty;
            Password = string.Empty;
            RefreshToken = string.Empty;
            UserId = 0;
            UserEmail = string.Empty;
        }
        public FitUser(SignUpDto input)
        {
            Username = input.Username;
            Password = input.Password;
            RefreshToken = string.Empty;
            Profile = input.UserProfile;
            UserEmail = input.UserEmail;
        }
        #endregion

        [Column("user_id")]
        [Key]
        public int UserId { get; set; }

        [Column("user_name")]
        public string Username { get; set; }

        [Column("user_password")]
        public string Password { get; set; }

        [Column("refresh_token")]
        public string RefreshToken { get; set; }

        [Column("profile")]
        public UserProfileEnum Profile { get; set; }

        [Column("user_email")]
        public string? UserEmail { get; set; }
    }
}