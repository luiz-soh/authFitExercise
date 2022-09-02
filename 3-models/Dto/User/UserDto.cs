using models.Entities.FitUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dto.User
{
    public class UserDto
    {
        public UserDto(FitUser user)
        {
            Id = user.UserId;
            Name = user.Username;
            IsEmailVerified = user.IsEmailVerified;
        }

        public UserDto() {
            Id = 0;
            Name = string.Empty;
            IsEmailVerified = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
