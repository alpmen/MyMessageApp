using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyMessageApp.Core.Authentication.JWT_Token.JwtModels
{
    public class JwtTokenValidator
    {
        private static string tokenSecretKey = "";
        public static IEnumerable<Claim> GetClaims(string token)
        {
            try
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                return (jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken)?.Claims;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}