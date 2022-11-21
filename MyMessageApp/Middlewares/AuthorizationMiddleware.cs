using MyMessageApp.Core.Context;
using MyMessageApp.Core.CustomAttributes;
using MyMessageApp.Core.Enumarations;
using MyMessageApp.Core.Exceptions;
using MyMessageApp.Core.Extensions;
using MyMessageApp.Service.PanelRolePageServices;
using System.Net;

namespace MyMessageApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private IPanelRolePageService _panelRolePagesService;
        private ApiContext _apiContext;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IPanelRolePageService panelRolePagesService, ApiContext apiContext)
        {
            _panelRolePagesService = panelRolePagesService;
            _apiContext = apiContext;

            await Authorization(context);

            await _next(context);
        }
        private async Task Authorization(HttpContext context)
        {
            EndpointMetadataCollection metadata = context?.GetEndpoint()?.Metadata;

            if (metadata != null)
            {
                var actionInfo = metadata.Where(x => x.GetType() == typeof(Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)).FirstOrDefault() as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

                if (actionInfo != null && !string.IsNullOrEmpty(actionInfo.ControllerName))
                {
                    #region Controller Authorization
                    var controllerAction = actionInfo.ControllerTypeInfo.CustomAttributes.Where(x => x.AttributeType == typeof(PageRoleActionAttribute)).Select(s => s.ConstructorArguments[0].Value).FirstOrDefault();

                    if (controllerAction != null && !await CheckPageRoleAuthorization(Convert.ToByte(controllerAction).ParseEnum<PageRoleActionType>(), actionInfo.ControllerName))
                        throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Access Denied!");
                    #endregion

                    #region Method Authorization
                    var methodAction = actionInfo.MethodInfo.CustomAttributes.Where(x => x.AttributeType == typeof(PageRoleActionAttribute)).Select(s => s.ConstructorArguments[0].Value).FirstOrDefault();

                    if (methodAction != null && !await CheckPageRoleAuthorization(Convert.ToByte(methodAction).ParseEnum<PageRoleActionType>(), actionInfo.ControllerName))
                        throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Access Denied!");
                    #endregion
                }
                else
                    throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Access Denied!");
            }
        }

        private async Task<bool> CheckPageRoleAuthorization(PageRoleActionType pageAction, string controllerName)
        {
            if (pageAction != PageRoleActionType.None)
            {
                if (_apiContext.UserId != 0)
                {
                    var rolePages = await _panelRolePagesService.ListByPanelUserIdAndPageName(_apiContext.UserId, controllerName);

                    if (rolePages == null)
                        return false;

                    foreach (var rolePage in rolePages)
                    {
                        if (pageAction == PageRoleActionType.Read && !rolePage.Read)
                            return false;

                        if (pageAction == PageRoleActionType.Write && !rolePage.Write)
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
