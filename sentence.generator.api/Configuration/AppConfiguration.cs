namespace sentence.generator.api.Configuration
{
    public class AppConfiguration
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
    }
}
