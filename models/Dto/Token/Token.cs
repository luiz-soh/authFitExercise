using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.Dto.Token
{
    public class Token
    {
        #region  construtores
        public Token(string user_Token, string refresh_Token)
        {
            this.User_Token = user_Token;
            this.Refresh_Token = refresh_Token;
        }

        public Token()
        {
            this.User_Token = string.Empty;
            this.Refresh_Token = string.Empty;
        }
        #endregion

        public string User_Token { get; set; }
        public string Refresh_Token { get; set; }
    }
}