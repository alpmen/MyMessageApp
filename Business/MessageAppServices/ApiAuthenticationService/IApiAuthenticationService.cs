using MyMessageApp.Data.AuthenticationRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MessageAppServices.ApiAuthenticationService
{
    public interface IApiAuthenticationService
    {
        Task<string> Login(ApiAuthenticationLoginRequest request);

    }
}
