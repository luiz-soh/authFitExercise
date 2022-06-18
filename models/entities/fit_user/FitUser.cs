using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace models.entities.fit_user
{
    public class FitUser
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string refresh_token { get; set; }
    }
}