using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMessageApp.Core.CustomAttributes;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Models;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using MyMessageApp.Service.MessageAppServices.UserRoleService;

namespace MyMessageApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [PageRoleAction(PageRoleActionType.Read)]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<UserRoleListAllResult>))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> ListAllUserRoles()
        {
            var results = await _userRoleService.ListAll();

            return Ok(results);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserRoleGetByIdResult))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userRoleService.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> Del(int id)
        {
            await _userRoleService.Remove(id);

            return NoContent();
        }

        [HttpPost]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task Create(UserRoleCreateRequest dto)
        {
            await _userRoleService.Create(dto);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> Update(UserRoleUpdateRequest dto)
        {
            await _userRoleService.Update(dto);

            return NoContent();
        }
    }
}