using Microsoft.Extensions.Options;
using MyMessageApp.Core.Authentication.JWT_Token.JwtModels;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Exceptions;
using MyMessageApp.Data.AuthenticationRepository.Dtos;
using MyMessageApp.Service.MessageAppServices.UserService;
using System.Net;

namespace MyMessageApp.Service.MessageAppServices.ApiAuthenticationService
{
    public class ApiAuthenticationService : IApiAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtTokenSettings> _jwtConfiguration;

        public ApiAuthenticationService(IUserService userService, IOptions<JwtTokenSettings> jwtConfiguration)
        {
            _userService = userService;
            _jwtConfiguration = jwtConfiguration;
        }

        public async Task<UsersLoginResult> Login(ApiAuthenticationLoginRequest request)
        {
            if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
            {
                var user = await _userService.GetByEmailandPassword(request.Email, request.Password);

                if (user != null)
                {
                    if (user.Status == (Byte)SystemStatusType.Active)
                    {
                        Dictionary<string, string> claims = new Dictionary<string, string>
                        {
                            { "Id", user.Id.ToString() },
                            { "Email", user.Email.ToString() },
                            { "Role", user.UserRoles.ToString() }
                        };
                        var tokenResult = JwtTokenGenerator.GenerateToken(claims,_jwtConfiguration.Value);

                        return new UsersLoginResult
                        {
                            AccessToken = tokenResult.AccessToken
                        };
                    }
                    throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "User Status Is Passive");
                }
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Email Or Password Is Wrong");
            }
            throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Email Or Password Is Empty");
        }
    }
}