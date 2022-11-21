using Data.MyMessageApp.Domain.EFDbContext.EFCoreRepository;

namespace Data.MyMessageApp.Domain.EFDbContext.EFCoreUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;  
        public IRepositoryBase<T> GetRepository<T>() where T : class, new()
        {
            return new RepositoryBase<T>(_context); 
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
