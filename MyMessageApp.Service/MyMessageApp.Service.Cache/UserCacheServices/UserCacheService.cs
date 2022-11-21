using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using MyMessageApp.Core.CacheServices;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;

namespace MyMessageApp.Service.MyMessageApp.Service.Cache.UserCacheServices
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

        public async Task<List<UserListResponse>> GetUserList()
        {
            List<UserListResponse> Users = new List<UserListResponse>();
            List<UserListResponse> cachedData = _cacheManager.Get<List<UserListResponse>>("UserList");
            if (cachedData != null && cachedData.Any())
            {
                Users = cachedData;
            }
            else
            {
                Users = _mapper.Map<List<UserListResponse>>(await _userRepository.GetAllAsync());
                var options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1),
                    Priority = CacheItemPriority.High
                };
                // Listeyi doldurduktan sonra Cache Manager ile kayıt işlemimizi yaptık.
                _cacheManager.Set<List<UserListResponse>>("UserList", Users);
            }
            return Users;
        }
    }
}
