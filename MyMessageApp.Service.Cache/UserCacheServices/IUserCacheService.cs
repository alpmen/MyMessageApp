using MyMessageApp.Data.UserRepository.Dtos.Response;

namespace MyMessageApp.Service.Cache.UserCacheServices
{
    public interface IUserCacheService
    {
        public Task<List<UsersListAllResponse>> GetUserList();
    }
}