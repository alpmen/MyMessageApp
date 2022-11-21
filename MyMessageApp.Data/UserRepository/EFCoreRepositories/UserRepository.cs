using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;

namespace MyMessageApp.Data.UserRepository.EFCoreRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(Message_App2Context context) : base(context)
        {
        }
    }
}