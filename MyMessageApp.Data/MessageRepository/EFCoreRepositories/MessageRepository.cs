using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using System.Linq;
using System.Linq.Expressions;

namespace MyMessageApp.Data.MessageRepository.EFCoreRepositories
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        private readonly Message_App2Context _messageRepository;
        private readonly IMapper _mapper;

        public MessageRepository(Message_App2Context context) : base(context)
        {
        }

        public async Task<List<MessagesListByUserIdAndFilterResult>> ListPrivate(int UserId, string content = null)
        {
            return await _collection.Select(x => new MessagesListByUserIdAndFilterResult
            {
                Id = x.Id,
                SenderEmail = x.Sender.Email,
                ReceiverId = x.ReceiverId,
                CreatedDate = (DateTime)x.Date,
                Content = x.Content
            }).Where(x => (x.ReceiverId == UserId || x.ReceiverId == null) && (content == null || x.Content.Contains(content))).ToListAsync();
        }

        public async Task<List<MessagesListAllResult>> ListAll()
        {
            return await _collection.Select(x => new MessagesListAllResult
            {
                Id = x.Id,
                SenderEmail = x.Sender.Email,
                ReceiverId = x.ReceiverId,
                CreatedDate = (DateTime)x.Date,
                Content = x.Content
            }).ToListAsync();
        }

        public async Task<MessagesGetDetailByIdResult> GetDetailById(object id)
        {
            return await _collection.Where(x => x.Id == (int)id).Select(x => new MessagesGetDetailByIdResult
            {
                SenderEmail = x.Sender.Email,
                ReceiverEmail = x.Receiver.Email,
                Id = x.Id,
                Content = x.Content
            }).SingleOrDefaultAsync();
        }
    }
}