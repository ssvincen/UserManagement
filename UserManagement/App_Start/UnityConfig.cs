using Microsoft.AspNetCore.Authorization;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UserManagement.Services;

namespace UserManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IConnectionManager, ConnectionManager>();
            container.RegisterType<IDataAccessService, DataAccessService>();
            container.RegisterType<IAuthorizationHandler, PermissionHandler>();
            container.RegisterType<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}