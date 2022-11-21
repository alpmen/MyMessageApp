using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using System.Linq.Expressions;

namespace Business.MessageAppServices.MessageService
{
    public interface IMessageService
    {
        Task<List<MessageListDto>> Getall();
        Task<List<MessageListDto>> GetByFilter(Expression<Func<Message, bool>> filter);
        Task Create(MessageCreateDto messageCreateDto);  
        Task<MessageListDto> GetById(object id);
        Task Remove(object id);
        Task Update(MessageUpdateDto dto);

    }
}
