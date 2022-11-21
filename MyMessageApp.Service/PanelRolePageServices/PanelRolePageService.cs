using MyMessageApp.Core.Exceptions;
using MyMessageApp.Data.Domain.EFDbContext.EFCoreUnitOfWork;
using MyMessageApp.Data.PanelRolePagesRepositories.Dtos.Result;
using MyMessageApp.Data.PanelRolePagesRepositories.EFCoreRepositories;
using System.Net;

namespace MyMessageApp.Service.PanelRolePageServices
{
    public class PanelRolePageService : IPanelRolePageService
    {
        private readonly IPanelRolePagesRepository _panelRolePagesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PanelRolePageService(IPanelRolePagesRepository panelRolePagesRepository, IUnitOfWork unitOfWork)
        {
            _panelRolePagesRepository = panelRolePagesRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delete panel role pages by panel role id
        /// </summary>
        /// <param name="panelRoleId"></param>
        /// <returns></returns>
        /// <exception cref="HttpStatusCodeException"></exception>
        public async Task DeleteByRoleId(int panelRoleId)
        {
            var rolePages = await _panelRolePagesRepository.FindListAsync(x => x.RoleId == panelRoleId);

            if (rolePages != null)
            {
                _panelRolePagesRepository.DeleteAll(rolePages);

                var deleteResult = await _unitOfWork.SaveChanges();

                if (deleteResult <= 0)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Delete Error");
            }
        }

        /// <summary>
        /// List panel role page list by panel user id and page name
        /// </summary>
        /// <param name="panelUserId"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public async Task<List<PanelRolePagesListByPanelUserIdAndPageNameResult>> ListByPanelUserIdAndPageName(int panelUserId, string pageName)
        {
            return await _panelRolePagesRepository.ListByPanelUserIdAndPageName(panelUserId, pageName);
        }
    }
}