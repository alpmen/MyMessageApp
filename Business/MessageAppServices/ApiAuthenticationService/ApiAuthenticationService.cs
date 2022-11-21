using AutoMapper;
using MyMessageApp.Core.Authentication.JWT_Token;
using MyMessageApp.Data.AuthenticationRepository.Dtos;

namespace Business.MessageAppServices.ApiAuthenticationService
{
    public class ApiAuthenticationService : IApiAuthenticationService
    {
        public async Task<string> Login(ApiAuthenticationLoginRequest request)
        {
            if (request.Email == "123@mail.com" && request.Password == "123")
            {
                return JwtTokenGenerator.GenerateToken(new Dictionary<string, string>
                {
                    {"Email",request.Email },
                    {"roles", "admin"}
                });
            }
            throw new Exception("user not found");
        }
    }
}