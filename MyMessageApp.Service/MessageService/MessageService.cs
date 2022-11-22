using AutoMapper;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Core.Context;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Exceptions;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;
using MyMessageApp.Service.Cache.MessageCacheServices;
using System.Net;

namespace MyMessageApp.Service.MessageAppServices.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IMessageCacheService _messageCacheService;
        private readonly ApiContext _apiContext;
        private readonly ICacheService _cacheManager;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IMessageRepository messageRepository, IMessageCacheService messageCacheService, ApiContext apiContext, ICacheService cacheManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageRepository = messageRepository;
            _messageCacheService = messageCacheService;
            _apiContext = apiContext;
            _cacheManager = cacheManager;
        }

        public async Task Create(MessageCreateRequest messageCreateDto)
        {
            var entity = _mapper.Map<Message>(messageCreateDto);

            entity.SenderId = _apiContext.UserId;

            entity.Date = DateTime.Now;

            var allMessagesKey = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.GeneralMessages.ToString(), _apiContext.UserId);

            var getByUserIdKey = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.UserMessages.ToString(), _apiContext.UserId);

            var getUsersByFilterKey = String.Format("{0}_{1}_{2}", CacheGroupType.Messages, CachePrefixType.UserMessagesByFilter.ToString(), _apiContext.UserId);

            if (messageCreateDto.ReceiverId == 0)
            {
                entity.ReceiverId = null;
            }

            _messageRepository.Add(entity);

            int result = await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "registration failed");
            }
            else
            {
                if (messageCreateDto.ReceiverId != 0)
                {
                    _cacheManager.Clear(getByUserIdKey);
                    _cacheManager.Clear(getUsersByFilterKey);
                }
                else
                {
                    _cacheManager.Clear(allMessagesKey);
                }
            }
        }

        public async Task<List<MessagesListAllResult>> ListAllMessages()
        {
            var list = await _messageCacheService.GetMessageList();

            return list;
        }

        public async Task<MessagesGetDetailByIdResult> GetById(object id)
        {
            return await _messageCacheService.GetById(id);
        }

        public async Task Remove(object id)
        {
            var deletedMessage = await _messageRepository.GetByID(id);

            if (deletedMessage != null)
            {
                _messageRepository.Delete(deletedMessage);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "entity not found");
            }

            int result = await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "delete failed");
            }
        }

        public async Task Update(MessageUpdateRequest dto)
        {
            var entity = _mapper.Map<Message>(dto);

            entity.SenderId = _apiContext.UserId;

            entity.Date = DateTime.Now;

            _messageRepository.Update(entity);

            int result = await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "update failed");
            }
        }

        public async Task<List<MessagesPrivateListByFilterResult>> PrivateListByFilter(string content)
        {
            return await _messageCacheService.GetUserMessageList(_apiContext.UserId, content);
        }

        public async Task<List<MessagesPrivateListByFilterResult>> PrivateList()
        {
            return await _messageCacheService.GetUserMessageList(_apiContext.UserId);
        }
    }
}