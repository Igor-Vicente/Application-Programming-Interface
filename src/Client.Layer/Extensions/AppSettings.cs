namespace Client.Layer.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; } = "";
        public int ExpirationHours { get; set; }
        public string Host { get; set; } = "";
        public string ValidAudience { get; set; } = ""; //audience
    }
}
