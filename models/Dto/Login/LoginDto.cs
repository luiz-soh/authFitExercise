namespace models.Dto.Login
{
    public class LoginDto
    {
        public LoginDto(string username, string password)
        {
            Username = username;
            Password = password;

        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}