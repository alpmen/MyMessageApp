using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMessageApp.Core.Authentication.JWT_Token.JwtModels
{
    public class JwtTokenGenerator
    {
        public static JWTResultModel GenerateToken(Dictionary<string, string> claims, JwtTokenSettings configuration)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.AccessTokenSecretKey));

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(configuration.AccessTokenSecretKey)), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256");

            List<Claim> tokenClaims = new List<Claim>();

            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    tokenClaims.Add(new Claim(claim.Key, claim.Value));
                }
            }

            JwtSecurityToken token = new JwtSecurityToken(issuer: configuration.Issuer, audience: configuration.Audience, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(configuration.Expire), signingCredentials: credentials, claims: tokenClaims);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new JWTResultModel
            {
                AccessToken = handler.WriteToken(token)
            };
        }
    }
}