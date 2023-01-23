namespace Models.Configuration.TokenConfiguration
{
    public class TokenConfiguration
    {
        public TokenConfiguration()
        {
            ClientSecret = string.Empty;
            PreSalt= string.Empty;
            PosSalt= string.Empty;
        }

        public const string Configuration = "TokenConfiguration";
        public string ClientSecret { get; set; }
        public string PreSalt { get; set; }
        public string PosSalt { get; set; }
    }
}