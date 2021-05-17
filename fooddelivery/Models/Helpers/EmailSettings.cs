namespace fooddelivery.Models.Helpers
{
    public class EmailSettings
    {
        public const string Position = "Email";

        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }

    }
}