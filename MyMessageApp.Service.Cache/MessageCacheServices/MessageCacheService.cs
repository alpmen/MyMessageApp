using AutoMapper;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Core.Context;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;

namespace MyMessageApp.Service.Cache.MessageCacheServices
{
    public class MessageCacheService : IMessageCacheService
    {
        private readonly ICacheService _cacheManager;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public MessageCacheService(ICacheService cacheManager, IMessageRepository messageRepository, IMapper mapper, ApiContext apiContext)
        {
            _cacheManager = cacheManager;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _apiContext = apiContext;
        }

        public async Task<List<MessagesListAllResult>> GetMessageList()
        {
            var key = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.GeneralMessages.ToString(), _apiContext.UserId);

            List<MessagesListAllResult> messages = new List<MessagesListAllResult>();

            List<MessagesListAllResult> cachedData = _cacheManager.Get<List<MessagesListAllResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                messages = cachedData;
            }
            else
            {
                messages = _mapper.Map<List<MessagesListAllResult>>(await _messageRepository.ListAll());

                _cacheManager.Set(key, messages);
            }

            return messages;
        }

        public async Task<List<MessagesListByUserIdAndFilterResult>> GetUserMessagesByFilter(int id)
        {
            var key = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.UserMessagesByFilter.ToString(), _apiContext.UserId);

            List<MessagesListByUserIdAndFilterResult> Messages = new List<MessagesListByUserIdAndFilterResult>();

            List<MessagesListByUserIdAndFilterResult> cachedData = _cacheManager.Get<List<MessagesListByUserIdAndFilterResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                Messages = cachedData;
            }
            else
            {
                Messages = _mapper.Map<List<MessagesListByUserIdAndFilterResult>>(await _messageRepository.ListPrivate(id));

                _cacheManager.Set(key, Messages);
            }
            return Messages;
        }

        public async Task<List<MessagesListByUserIdAndFilterResult>> GetUserMessageList(int id)
        {
            var key = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.UserMessages.ToString(), _apiContext.UserId);

            List<MessagesListByUserIdAndFilterResult> Messages = new List<MessagesListByUserIdAndFilterResult>();

            List<MessagesListByUserIdAndFilterResult> cachedData = _cacheManager.Get<List<MessagesListByUserIdAndFilterResult>>(key);

            if (cachedData != null && cachedData.Any())
            {
                Messages = cachedData;
            }
            else
            {
                Messages = _mapper.Map<List<MessagesListByUserIdAndFilterResult>>(await _messageRepository.ListPrivate(id));

                _cacheManager.Set(key, Messages);
            }
            return Messages;
        }

        public async Task<MessagesGetDetailByIdResult> GetById(object id)
        {
            var key = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.UserMessages.ToString(), id);

            MessagesGetDetailByIdResult messages = new MessagesGetDetailByIdResult();

            MessagesGetDetailByIdResult cachedData = _cacheManager.Get<MessagesGetDetailByIdResult>(key);

            if (cachedData != null)
            {
                messages = cachedData;
            }
            else
            {
                messages = _mapper.Map<MessagesGetDetailByIdResult>(await _messageRepository.GetDetailById(id));

                _cacheManager.Set(key, messages);
            }
            return messages;
        }
    }
}