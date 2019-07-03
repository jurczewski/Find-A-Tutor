namespace Find_A_Tutor.Infrastructure.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
