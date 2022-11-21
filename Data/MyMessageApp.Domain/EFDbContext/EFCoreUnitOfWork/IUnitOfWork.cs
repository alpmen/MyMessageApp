using Data.MyMessageApp.Domain.EFDbContext.EFCoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyMessageApp.Domain.EFDbContext.EFCoreUnitOfWork
{
    public interface IUnitOfWork
    {
        IRepositoryBase<T> GetRepository<T>() where T : class, new();
        Task SaveChanges();
    }
}
