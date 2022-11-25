using MyMessageApp.Data.MessageRepository.Dtos.Response;

namespace MyMessageApp.Service.Cache.MessageCacheServices
{
    public interface IMessageCacheService
    {
        public Task<List<MessagesListAllResult>> GetMessageList();
        public Task<List<MessagesListByUserIdAndFilterResult>> GetUserMessagesByFilter(int id);
        public Task<List<MessagesListByUserIdAndFilterResult>> GetUserMessageList(int id);
        public Task<MessagesGetDetailByIdResult> GetById(object id);
    }
}