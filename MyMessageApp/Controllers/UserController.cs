using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMessageApp.Core.CustomAttributes;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Models;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Service.MessageAppServices.UserService;

namespace MyMessageApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [PageRoleAction(PageRoleActionType.Read)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UsersListAllResult>))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> ListAll()
        {
            var results = await _userService.ListAll();

            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserGetByIdResponse))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task Create(UserCreateRequest dto)
        {
            await _userService.Create(dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest dto, int id)
        {
            await _userService.Update(dto, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> Remove(int id)
        {
            await _userService.Remove(id);

            return NoContent();
        }
    }
}