namespace fooddelivery.Models.Helpers
{
    public class JWTSettings
    {
        public const string Position = "Auth:JWTBearer";
        public string SecretKey { get; set; }
    }
}