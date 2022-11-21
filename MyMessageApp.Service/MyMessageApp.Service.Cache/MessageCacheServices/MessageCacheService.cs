using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Core.Context;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;
using MyMessageApp.Service.MyMessageApp.Service.Cache.UserCacheServices;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Service.MyMessageApp.Service.Cache.MessageCacheServices
{
    public class MessageCacheService : IMessageCacheService
    {
        private readonly ICacheService _cacheManager;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly ApiContext _apiContext;

        public MessageCacheService(ICacheService cacheManager, IMessageRepository messageRepository, IMapper mapper)
        {
            _cacheManager = cacheManager;
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<List<MessageListResult>> GetMessageList()
        {
            List<MessageListResult> Messages = new List<MessageListResult>();
            List<MessageListResult> cachedData = _cacheManager.Get<List<MessageListResult>>("MessageList");
            if (cachedData != null && cachedData.Any())
            {
                Messages = cachedData;
            }
            else
            {
                Messages = _mapper.Map<List<MessageListResult>>(await _messageRepository.GetAllAsync());
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High
                };
                // Listeyi doldurduktan sonra Cache Manager ile kayıt işlemimizi yaptık.
                _cacheManager.Set<List<MessageListResult>>("MessageList", Messages);
            }
            return Messages;
        }
        public async Task<List<PrivateMessageListResult>> GetUserMessageList(int id)
        {
            List<PrivateMessageListResult> Messages = new List<PrivateMessageListResult>();
            List<PrivateMessageListResult> cachedData = _cacheManager.Get<List<PrivateMessageListResult>>("MessageList");
            if (cachedData != null && cachedData.Any())
            {
                Messages = cachedData;
            }
            else
            {
                Messages = _mapper.Map<List<PrivateMessageListResult>>(await _messageRepository.ListPrivate(id));
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High
                };
                // Listeyi doldurduktan sonra Cache Manager ile kayıt işlemimizi yaptık.
                _cacheManager.Set<List<PrivateMessageListResult>>("MessageList", Messages);
            }
            return Messages;
        }

        public async Task<List<PrivateMessageListResult>> FindListAsync(string Content,int id)
        {
            List<PrivateMessageListResult> Messages = new List<PrivateMessageListResult>();
            List<PrivateMessageListResult> cachedData = _cacheManager.Get<List<PrivateMessageListResult>>("MessageList");
            if (cachedData != null && cachedData.Any())
            {
                Messages = cachedData;
            }
            else
            {
                Messages =_mapper.Map<List<PrivateMessageListResult>>(await _messageRepository.GetByCache(Content,id));
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High
                };
                // Listeyi doldurduktan sonra Cache Manager ile kayıt işlemimizi yaptık.
                _cacheManager.Set<List<PrivateMessageListResult>>("MessageList", Messages);
            }
            return Messages;
        }


    }
}
