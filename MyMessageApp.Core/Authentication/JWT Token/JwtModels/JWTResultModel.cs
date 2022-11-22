namespace MyMessageApp.Core.Authentication.JWT_Token.JwtModels
{
    public class JWTResultModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
