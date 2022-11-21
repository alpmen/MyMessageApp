using System.Linq.Expressions;

namespace MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class, new()
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entity);
        void Update(TEntity entity);
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> FindAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> FindListAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByID(object id);
        Task<(List<TEntity> Data, long TotalCount)> SearchAsync(IQueryable<TEntity> query, int? pageNumber, int? pageSize);
        Task<(IQueryable<TEntity> Query, long TotalCount)> QueryPagination(IQueryable<TEntity> query, int? pageNumber, int? pageSize);

    }
}
