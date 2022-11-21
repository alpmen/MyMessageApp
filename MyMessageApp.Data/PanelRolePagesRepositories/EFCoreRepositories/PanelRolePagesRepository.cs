using Microsoft.EntityFrameworkCore;
using MyMessageApp.Data.Domain.EFDbContext;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreRepository;
using MyMessageApp.Data.Domain.Entities;
using MyMessageApp.Data.PanelRolePagesRepositories.Dtos.Result;

namespace MyMessageApp.Data.PanelRolePagesRepositories.EFCoreRepositories
{
    public class PanelRolePagesRepository : RepositoryBase<RolePage>, IPanelRolePagesRepository
    {
        public PanelRolePagesRepository(Message_App2Context context) : base(context)
        {
        }

        /// <summary>
        /// List panel role page list by panel user id and page name
        /// </summary>
        /// <param name="panelUserId"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public async Task<List<PanelRolePagesListByPanelUserIdAndPageNameResult>> ListByPanelUserIdAndPageName(int panelUserId, string pageName)
        {
            return await _collection.Where(x =>
                x.Role.UserRoles.Any(y => y.UserId == panelUserId) &&
                x.Page.Name == pageName)
                .Select(x => new PanelRolePagesListByPanelUserIdAndPageNameResult
                {
                    Read = (bool)x.Read,
                    Write = (bool)x.Write
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}