using AutoMapper;
using MyMessageApp.Core.Exceptions;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using MyMessageApp.Data.UserRoleRepository.EFCoreRepositories;
using System.Linq.Expressions;
using System.Net;

namespace MyMessageApp.Service.MessageAppServices.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUnitOfWork unitOfWork, IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
        }

        public async Task Create(UserRoleCreateRequest userRoleCreateDto)
        {
            _userRoleRepository.Add(_mapper.Map<UserRole>(userRoleCreateDto));

            int result=await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "create failed");
            }
        }

        public async Task<List<UserRoleListAllResult>> ListAll()
        {
            return _mapper.Map<List<UserRoleListAllResult>>(await _userRoleRepository.GetAllAsync());
        }

        public async Task<List<UserRoleGetByFilterResult>> GetByFilter(Expression<Func<UserRole, bool>> filter)
        {
            return _mapper.Map<List<UserRoleGetByFilterResult>>(await _userRoleRepository.FindListAsync(filter));
        }

        public async Task<UserRoleGetByIdResult> GetById(object id)
        {
            return _mapper.Map<UserRoleGetByIdResult>(await _userRoleRepository.GetByID(id));
        }

        public async Task Remove(object id)
        {
            var deletedUserRole = await _userRoleRepository.GetByID(id);

            if (deletedUserRole != null)
            {
                _userRoleRepository.Delete(deletedUserRole);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "entity not found");
            }

            int result=await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "delete failed");
            }
        }

        public async Task Update(UserRoleUpdateRequest dto)
        {
            _userRoleRepository.Update(_mapper.Map<UserRole>(dto));

            int result=await _unitOfWork.SaveChanges();

            if (result <= 0)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "update failed");
            }
        }
    }
}