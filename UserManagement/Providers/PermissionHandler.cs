using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Web;

namespace UserManagement
{

    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        public AuthorizationRequirement(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; }
    }
    public class PermissionHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IDataAccessService _dataAccessService;

        public PermissionHandler(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            //if (context.Resource is RouteEndpoint endpoint)
            //{
            //	endpoint.RoutePattern.RequiredValues.TryGetValue("controller", out var _controller);
            //	endpoint.RoutePattern.RequiredValues.TryGetValue("action", out var _action);

            //	endpoint.RoutePattern.RequiredValues.TryGetValue("page", out var _page);
            //	endpoint.RoutePattern.RequiredValues.TryGetValue("area", out var _area);

            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            string _controller = null;
            string _action = null;
            if (routeValues.ContainsKey("controller"))
            {
                _controller = (string)routeValues["controller"];
            }
            if (routeValues.ContainsKey("action"))
            {
                _action = (string)routeValues["action"];
            }

            if (!string.IsNullOrWhiteSpace(requirement?.PermissionName) && !requirement.PermissionName.Equals("Authorization"))
            {
                _action = requirement.PermissionName;
            }

            if (context.User.Identity.IsAuthenticated && _controller != null && _action != null &&
                await _dataAccessService.GetMenuItemsAsync(context.User, _controller.ToString(), _action.ToString()))
            {
                context.Succeed(requirement);
            }

            await Task.CompletedTask;
        }

    }
}