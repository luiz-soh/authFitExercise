using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Dto.Login;
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
            UserId = 0;
        }
        public FitUser(LoginDto input)
        {
            Username = input.Username;
            Password = input.Password;
            RefreshToken = string.Empty;
            Profile = input.UserProfile; //Passar para enum e criar no DTO
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
    }
}