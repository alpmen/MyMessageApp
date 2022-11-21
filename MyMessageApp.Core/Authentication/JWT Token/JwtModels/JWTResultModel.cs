using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Core.Authentication.JWT_Token.JwtModels
{
    public class JWTResultModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
