using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.UserRoleRepository.Dtos.Request;
using MyMessageApp.Data.UserRoleRepository.Dtos.Response;
using System.Linq.Expressions;

namespace Business.MessageAppServices.UserRoleService
{
    public interface IUserRoleService
    {
        Task<List<UserRoleListDto>> Getall();
        Task<List<UserRoleListDto>> GetByFilter(Expression<Func<UserRole, bool>> filter);
        Task Create(UserRoleCreateDto userRoleCreateDto);
        Task<UserRoleListDto> GetById(object id);
        Task Remove(object id);
        Task Update(UserRoleUpdateDto dto);
    }
}
