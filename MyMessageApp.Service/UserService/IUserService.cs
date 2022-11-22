using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRepository.Dtos.Request;
using MyMessageApp.Data.UserRepository.Dtos.Response;

namespace MyMessageApp.Service.MessageAppServices.UserService
{
    public interface IUserService
    {
        Task<List<UsersListAllResult>> ListAll();
        Task Create(UserCreateRequest userCreateDto);
        Task<UserGetByIdResponse> GetById(object id);
        Task Remove(object id);
        Task Update(UserUpdateRequest dto, int id);
        Task<User> GetByEmailandPassword(string email, string password);
    }
}