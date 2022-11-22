using AutoMapper;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;

namespace MyMessageApp.Service.Mappers
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateRequest>().ReverseMap();
            CreateMap<Message, MessageUpdateRequest>().ReverseMap();
            CreateMap<Message, MessagesListAllResult>().ForMember(dest => dest.SenderEmail, opt => opt.MapFrom(src => src.Sender.Email)).ReverseMap();
            CreateMap<Message, MessagesListByUserIdAndFilterResult>().ReverseMap();
            CreateMap<Message, MessagesListByUserIdResult>().ReverseMap();
        }
    }
}
