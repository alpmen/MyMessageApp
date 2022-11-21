namespace MyMessageApp.Core.Authentication.JWT_Token.JwtModels
{
    public class JwtTokenSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessTokenSecretKey { get; set; }
        public int Expire { get; set; } 
    }
}