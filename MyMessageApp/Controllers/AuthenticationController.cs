using Microsoft.AspNetCore.Mvc;
using MyMessageApp.Core.CustomAttributes;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Data.AuthenticationRepository.Dtos;
using MyMessageApp.Service.MessageAppServices.ApiAuthenticationService;

namespace MyMessageApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [PageRoleAction(PageRoleActionType.None)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IApiAuthenticationService _apiAuthenticationService;

        public AuthenticationController(IApiAuthenticationService apiAuthenticationService)
        {
            _apiAuthenticationService = apiAuthenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ApiAuthenticationLoginRequest request)
        {
            return Ok(await _apiAuthenticationService.Login(request));
        }
    }
}