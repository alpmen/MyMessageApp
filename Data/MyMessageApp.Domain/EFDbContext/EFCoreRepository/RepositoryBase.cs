using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.MyMessageApp.Domain.EFDbContext.EFCoreRepository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        private readonly Context _c;

        public RepositoryBase(Context c)
        {
            _c = c;
        }

        public async Task Create(T Entity)
        {
            await _c.Set<T>().AddAsync(Entity);
        }

        public void Delete(int id)
        {
            var entity=_c.Set<T>().Find(id);
            _c.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _c.Set<T>().AsNoTracking().ToListAsync();      
        }

        public async Task<List<T>> GetBtFilter(Expression<Func<T, bool>> filter)
        {
            return await _c.Set<T>().Where(filter).ToListAsync();  
        }

        public async Task<T> GetById(int id)
        {
            return await _c.Set<T>().FindAsync(id);
        }

        public void Update(T Entity)
        {
            _c.Set<T>().Update(Entity);
        }
    }
}
