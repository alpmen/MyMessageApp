using MyMessageApp.Data.AuthenticationRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Service.MessageAppServices.ApiAuthenticationService
{
    public interface IApiAuthenticationService
    {
        Task<UsersLoginResult> Login(ApiAuthenticationLoginRequest request);
    }
}
