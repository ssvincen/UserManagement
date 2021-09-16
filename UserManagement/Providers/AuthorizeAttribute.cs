using System;
using System.Web;
using System.Web.Mvc;

namespace UserManagement.Providers
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
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

            throw new NotImplementedException();
        }
    }
}