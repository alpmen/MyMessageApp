using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.PanelRolePagesRepositories.Dtos.Result;

namespace MyMessageApp.Data.PanelRolePagesRepositories.EFCoreRepositories
{
    public interface IPanelRolePagesRepository : IRepositoryBase<RolePage>
    {
        /// <summary>
        /// List panel role page list by panel user id and page name
        /// </summary>
        /// <param name="panelUserId"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        Task<List<PanelRolePagesListByPanelUserIdAndPageNameResult>> ListByPanelUserIdAndPageName(int panelUserId, string pageName);
    }
}