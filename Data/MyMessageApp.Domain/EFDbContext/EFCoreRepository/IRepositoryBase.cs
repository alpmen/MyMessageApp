using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyMessageApp.Domain.EFDbContext.EFCoreRepository
{
    public interface IRepositoryBase<T> where T: class, new()
    {
        Task<List<T>> GetAll(); 
        Task<T> GetById(int id);
        Task<List<T>> GetBtFilter(Expression<Func<T,bool>>filter);
        Task Create(T Entity);
        void Update(T Entity);  
        void Delete(int id);

    }
}
