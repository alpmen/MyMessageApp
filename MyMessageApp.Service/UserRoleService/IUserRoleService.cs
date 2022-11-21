using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using System.Linq.Expressions;

namespace MyMessageApp.Service.MessageAppServices.UserRoleService
{
    public interface IUserRoleService
    {
        Task<List<UserRoleListAllResult>> ListAll();
        Task<List<UserRoleGetByFilterResult>> GetByFilter(Expression<Func<UserRole, bool>> filter);
        Task Create(UserRoleCreateRequest userRoleCreateDto);
        Task<UserRoleGetByIdResult> GetById(object id);
        Task Remove(object id);
        Task Update(UserRoleUpdateRequest dto);
    }
}