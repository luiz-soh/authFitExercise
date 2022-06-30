namespace models.Dto.Login
{
    public class LoginInput
    {
        public LoginInput()
        {
            Username = string.Empty;
            Password = string.Empty;

        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}