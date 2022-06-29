namespace models.Configuration.TokenConfiguration
{
    public class TokenConfiguration
    {
        public TokenConfiguration()
        {
        }

        public const string Configuration = "TokenConfiguration";
        public string ClientSecret { get; set; } = string.Empty;
        public string PreSalt { get; set; } = string.Empty;
        public string PosSalt { get; set; } = string.Empty;
    }
}