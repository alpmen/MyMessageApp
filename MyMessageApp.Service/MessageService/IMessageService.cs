using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;

namespace MyMessageApp.Service.MessageAppServices.MessageService
{
    public interface IMessageService
    {
        Task Create(MessageCreateRequest messageCreateDto);
        Task<MessagesGetDetailByIdResult> GetById(object id);
        Task Remove(object id);
        Task Update(MessageUpdateRequest dto);
        Task<List<MessagesListAllResult>> ListAllMessages();
        Task<List<MessagesListByUserIdAndFilterResult>> ListByUserIdAndFilter(string content);
        Task<List<MessagesListByUserIdAndFilterResult>> ListByUserId();
    }
}