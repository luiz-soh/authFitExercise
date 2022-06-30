using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Dto.Login;

namespace models.Entities.FitUser
{
    [Table("fit_users")]
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
        }
        #endregion

        [Column("user_id")]
        [Key]
        public int UserId { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}