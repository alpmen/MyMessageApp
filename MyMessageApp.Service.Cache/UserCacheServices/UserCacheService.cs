using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;

namespace MyMessageApp.Service.Cache.UserCacheServices
{
    public class UserCacheService : IUserCacheService
    {
        private readonly ICacheService _cacheManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCacheService(ICacheService cacheManager, IMapper mapper, IUserRepository userRepository)
        {
            _cacheManager = cacheManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UsersListAllResponse>> GetUserList()
        {
            List<UsersListAllResponse> Users = new List<UsersListAllResponse>();
            List<UsersListAllResponse> cachedData = _cacheManager.Get<List<UsersListAllResponse>>("UserList");
            if (cachedData != null && cachedData.Any())
            {
                Users = cachedData;
            }
            else
            {
                Users = _mapper.Map<List<UsersListAllResponse>>(await _userRepository.GetAllAsync());
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High
                };
                // Listeyi doldurduktan sonra Cache Manager ile kayıt işlemimizi yaptık.
                _cacheManager.Set<List<UsersListAllResponse>>("UserList", Users);
            }
            return Users;
        }
    }
}
