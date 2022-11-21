using AutoMapper;
using MyMessageApp.Core.Context;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Exceptions;
using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;
using MyMessageApp.Service.Cache.UserCacheServices;
using System.Net;

namespace MyMessageApp.Service.MessageAppServices.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly Message_App2Context _context;
        private readonly IUserCacheService _userCacheService;
        private readonly ApiContext _apiContext;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository, Message_App2Context context, IUserCacheService userCacheService, ApiContext apiContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
            _userCacheService = userCacheService;
            _apiContext = apiContext;
        }

        public async Task Create(UserCreateRequest userCreateDto)
        {
            var entity = _mapper.Map<User>(userCreateDto);

            entity.CreatedBy = _apiContext.UserId;

            entity.Status = (byte)SystemStatusType.Active;

            entity.CreatedDate = DateTime.Now;

            _userRepository.Add(entity);

            int result = await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "create failed");
            }
        }

        public async Task<List<UsersListAllResponse>> ListAll()
        {
            return _mapper.Map<List<UsersListAllResponse>>(await _userCacheService.GetUserList());
        }

        public async Task<User> GetByEmailandPassword(string email, string password)
        {
            return await _userRepository.FindAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<UserGetByIdResponse> GetById(object id)
        {
            return _mapper.Map<UserGetByIdResponse>(await _userRepository.GetByID(id));
        }

        public async Task Remove(object id)
        {
            var entity = await _userRepository.GetByID(id);

            if (entity != null)
            {
                entity.Status = (byte)SystemStatusType.Deleted;

                entity.UpdatedDate = DateTime.Now;
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

        public async Task Update(UserUpdateRequest dto, int id)
        {
            var entity = await _userRepository.GetByID(id);

            entity.Status = dto.Status;

            entity.Email = dto.Email;

            entity.Password = dto.Password;

            entity.UpdatedBy = _apiContext.UserId;

            entity.UpdatedDate = DateTime.Now;

            int result = await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "update failed");
            }
        }
    }
}