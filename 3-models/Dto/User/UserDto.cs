using models.Entities.FitUser;

namespace models.Dto.User
{
    public class UserDto
    {
        public UserDto(FitUser user)
        {
            Id = user.UserId;
            Name = user.Username;
        }

        public UserDto() {
            Id = 0;
            Name = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
