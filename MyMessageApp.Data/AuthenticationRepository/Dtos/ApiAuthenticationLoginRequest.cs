using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Data.AuthenticationRepository.Dtos
{
    public class ApiAuthenticationLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}