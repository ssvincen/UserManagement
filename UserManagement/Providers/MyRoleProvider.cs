using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using UserManagement.Models;

namespace UserManagement
{
    public class MyRoleProvider : RoleProvider
    {
        IDataAccessService _dataAccess;
        private int _cacheTimeoutInMinute = 20;


        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }


        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }
            var cacheKey = string.Format("{0}_role", username);
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                List<string> list = (List<string>)HttpRuntime.Cache[cacheKey];
                return list.ToArray();
            }

            var output = new List<NavigationMenuView>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UserManagementDBEntities"].ConnectionString))
            {
                output = connection.Query<NavigationMenuView>("dbo.spr_GetPermissionsByUsername @EmailId", new { EmailId = username }).ToList();
            }

            foreach (var item in output)
            {
                roles.Add(item.Name);
            }

            if (roles.Count() > 0)
            {
                HttpRuntime.Cache.Insert(cacheKey, roles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinute), Cache.NoSlidingExpiration);
            }
            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}