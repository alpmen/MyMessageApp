using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Response;

namespace MyMessageApp.Data.MessageRepository.EFCoreRepositories
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        Task<List<MessagesListAllResult>> ListAll();
        Task<List<MessagesPrivateListByFilterResult>> ListPrivate(int id, string content = null);
        Task<MessagesGetDetailByIdResult> GetDetailById(object id);
    }
}