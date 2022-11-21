using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;

namespace MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> GetRepository<T>() where T : class, new();
        Task<int> SaveChanges();
    }
}
