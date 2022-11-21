using AutoMapper;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.MessageRepository.EFCoreRepositories;
using System.Linq.Expressions;

namespace Business.MessageAppServices.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IMessageRepository messageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task Create(MessageCreateDto messageCreateDto)
        {
            _messageRepository.Add(_mapper.Map<Message>(messageCreateDto));
            await _unitOfWork.SaveChanges();
        }

        public async Task<List<MessageListDto>> Getall()
        {
            
            return _mapper.Map<List<MessageListDto>>(await _messageRepository.GetAllAsync());
        }

        public async Task<List<MessageListDto>> GetByFilter(Expression<Func<Message, bool>> filter)
        {
            return _mapper.Map<List<MessageListDto>>(await _messageRepository.FindListAsync(filter));
        }

        public async Task<MessageListDto> GetById(object id)
        {
            return _mapper.Map<MessageListDto>(await _messageRepository.GetByID(id));
            
        }

        public async Task Remove(object id)
        {
            var deletedMessage =await _messageRepository.GetByID(id);
            _messageRepository.Delete(deletedMessage);
            _unitOfWork.SaveChanges();
        }
        
        public async Task Update(MessageUpdateDto dto)
        {
            _messageRepository.Update(_mapper.Map<Message>(dto));
            await _unitOfWork.SaveChanges();
        }
    }
}
