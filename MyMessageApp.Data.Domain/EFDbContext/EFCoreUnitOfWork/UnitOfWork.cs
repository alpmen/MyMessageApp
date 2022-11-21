using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;

namespace MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Message_App2Context _context;

        public UnitOfWork(Message_App2Context context)
        {
            _context = context;
        }

        public IRepositoryBase<T> GetRepository<T>() where T : class, new()
        {
            return new RepositoryBase<T>(_context);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}