using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.Dto.Login
{
    public class LoginInput
    {
        public LoginInput()
        {
            this.Username = string.Empty;
            this.Password = string.Empty;

        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}