using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMessageApp.Core.CustomAttributes;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Models;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Service.MessageAppServices.MessageService;

namespace MyMessageApp.Controllers
{
    [PageRoleAction(PageRoleActionType.Read)]
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> Remove(int id)
        {
            await _messageService.Remove(id);

            return NoContent();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(MessagesGetDetailByIdResult))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _messageService.GetById(id);

            return Ok(result);
        }

        [HttpGet("listall")]
        [ProducesResponseType(200, Type = typeof(List<MessagesListAllResult>))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> ListAll()
        {
            var messageList = await _messageService.ListAllMessages();

            return Ok(messageList);
        }

        [HttpGet("search/{content}")]
        [ProducesResponseType(200, Type = typeof(List<MessagesListByUserIdAndFilterResult>))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> GetByFilter(string content)
        {
            var result = await _messageService.ListByUserIdAndFilter(content);

            return Ok(result);
        }

        [HttpPost]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task Create([FromBody] MessageCreateRequest messageCreateDto)
        {
            await _messageService.Create(messageCreateDto);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(404, Type = typeof(ErrorResultModel))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        [PageRoleAction(PageRoleActionType.Write)]
        public async Task<IActionResult> Update([FromBody] MessageUpdateRequest updateDto)
        {
            await _messageService.Update(updateDto);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MessagesListByUserIdResult>))]
        [ProducesResponseType(500, Type = typeof(ErrorResultModel))]
        public async Task<IActionResult> ListByUserId()
        {
            var results = await _messageService.ListByUserId();

            return Ok(results);
        }
    }
}