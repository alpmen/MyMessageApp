using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMessageApp.Service.MyMessageApp.Service.Cache.MessageCacheServices
{
    public interface IMessageCacheService
    {
        public Task<List<MessageListResult>> GetMessageList();
        public Task<List<PrivateMessageListResult>> GetUserMessageList(int id);
        public Task<List<PrivateMessageListResult>> FindListAsync(string Content,int id);
    }
}
