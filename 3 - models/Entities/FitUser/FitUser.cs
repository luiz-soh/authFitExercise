using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Dto.Login;
using Models.Dto.Login.Register;
using Models.Enums;

namespace models.Entities.FitUser
{
    [Table("fituser")]
    public class FitUser
    {

        #region Contrutores

        public FitUser()
        {
            Username = string.Empty;
            Password = string.Empty;
            RefreshToken = string.Empty;
            UserEmail = string.Empty;
            UserId = 0;
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

        [Column("UserId")]
        [Key]
        public int UserId { get; set; }

        [Column("UserName")]
        public string Username { get; set; }

        [Column("UserPassword")]
        public string Password { get; set; }

        [Column("RefreshToken")]
        public string RefreshToken { get; set; }

        [Column("UserProfile")]
        public UserProfileEnum Profile { get; set; }

        [Column("UserEmail")]
        public string UserEmail { get; set; }

        [Column("IsEmailVerified")]
        public bool IsEmailVerified { get; set; }
    }
}