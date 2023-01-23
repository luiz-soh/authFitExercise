namespace application.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        string EncryptPassword(string password);
        string GenerateToken(string name, string role, int validyHours);
    }
}