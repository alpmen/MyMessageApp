using AutoMapper;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Request;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateDto>().ReverseMap();
            CreateMap<Message, MessageUpdateDto>().ReverseMap();
            CreateMap<Message, MessageListDto>().ReverseMap();
        }
    }
}
