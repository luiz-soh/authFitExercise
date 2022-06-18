namespace models.Entities.Fit_user
{
    public class FitUser
    {

        public int user_id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string refresh_token { get; set; } = string.Empty;
    }
}