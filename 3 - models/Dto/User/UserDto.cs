using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace models.Dto.User
{
    public class UserDto
    {
        public UserDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public UserDto() {
            Id = 0;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
