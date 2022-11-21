using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;

namespace MyMessageApp.Data.UserRoleRepository.EFCoreRepositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(Message_App2Context context) : base(context)
        {
        }
    }
}