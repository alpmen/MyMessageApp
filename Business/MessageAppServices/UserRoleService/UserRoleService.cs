using AutoMapper;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.MessageRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using MyMessageApp.Data.UserRoleRepository.EFCoreRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.MessageAppServices.UserRoleService
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

        public async Task Create(UserRoleCreateDto userRoleCreateDto)
        {
            _userRoleRepository.Add(_mapper.Map<UserRole>(userRoleCreateDto));
            _unitOfWork.SaveChanges();
        }

        public async Task<List<UserRoleListDto>> Getall()
        {
            return _mapper.Map<List<UserRoleListDto>>(await _userRoleRepository.GetAllAsync());
        }

        public async Task<List<UserRoleListDto>> GetByFilter(Expression<Func<UserRole, bool>> filter)
        {
            return _mapper.Map<List<UserRoleListDto>>(await _userRoleRepository.FindListAsync(filter));
        }

        public async Task<UserRoleListDto> GetById(object id)
        {
            return _mapper.Map<UserRoleListDto>(await _userRoleRepository.GetByID(id));
        }

        public async Task Remove(object id)
        {
            var deletedUserRole = await _userRoleRepository.GetByID(id);
            _userRoleRepository.Delete(deletedUserRole);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(UserRoleUpdateDto dto)
        {
            _userRoleRepository.Update(_mapper.Map<UserRole>(dto));
            await _unitOfWork.SaveChanges();
        }
    }
}
