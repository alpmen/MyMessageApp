using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;

namespace MyMessageApp.Data.UserRepository.EFCoreRepositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }
}