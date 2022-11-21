using AutoMapper;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.Enumarations;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;
using MyMessageApp.Data.UserRepository.EFCoreRepositories;

namespace Business.MessageAppServices.UserService
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task Create(UserCreateDto userCreateDto)
        {
            _userRepository.Add(_mapper.Map<User>(userCreateDto));
            _unitOfWork.SaveChanges();
        }

        public async Task<List<UserListDto>> Getall()
        {
            return _mapper.Map<List<UserListDto>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserListDto> GetById(object id)
        {
            return _mapper.Map<UserListDto>(await _userRepository.GetByID(id));
        }

        //set status passive
        public async Task Remove(object id)
        {
            var deletedEntity = await _userRepository.GetByID(id);

            deletedEntity.Status = (byte)SystemStatusType.Passive;
            deletedEntity.UpdatedDate = DateTime.Now;

            _userRepository.Update(deletedEntity);
            _unitOfWork.SaveChanges();
        }

        public async Task Update(UserUpdateDto dto)
        {
            _userRepository.Update(_mapper.Map<User>(dto));
            await _unitOfWork.SaveChanges();    
        }
    }
}
