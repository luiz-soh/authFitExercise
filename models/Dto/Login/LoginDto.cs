using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.Dto.Login
{
    public class LoginDto
    {
        public LoginDto(string username, string password)
        {
            this.Username = username;
            this.Password = password;

        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}