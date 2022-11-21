using MyMessageApp.Data.PanelRolePagesRepositories.Dtos.Result;

namespace MyMessageApp.Service.PanelRolePageServices
{
    public interface IPanelRolePageService
    {
        Task DeleteByRoleId(int panelRoleId);
        Task<List<PanelRolePagesListByPanelUserIdAndPageNameResult>> ListByPanelUserIdAndPageName(int panelUserId, string pageName);
    }
}