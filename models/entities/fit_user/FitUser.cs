using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Dto.Login;

namespace models.Entities.Fit_user
{
    [Table("fit_users")]
    public class FitUser
    {

        #region Contrutores

        public FitUser()
        {
            this.username = string.Empty;
            this.password = string.Empty;
            this.refresh_token = string.Empty;
            this.user_id = 0;
        }
        public FitUser(LoginDto input)
        {
            this.username = input.Username;
            this.password = input.Password;
            this.refresh_token = string.Empty;
        }
        #endregion

        [Column("user_id")]
        [Key]
        public int user_id { get; set; }

        [Column("username")]
        public string username { get; set; } = string.Empty;

        [Column("password")]
        public string password { get; set; } = string.Empty;

        [Column("refresh_token")]
        public string refresh_token { get; set; } = string.Empty;
    }
}