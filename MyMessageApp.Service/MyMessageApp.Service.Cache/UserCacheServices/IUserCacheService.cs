using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRepository.Dtos.Response;

namespace MyMessageApp.Service.MyMessageApp.Service.Cache.UserCacheServices
{
    public interface IUserCacheService
    {
        public Task<List<UserListResponse>> GetUserList();

    }
}
