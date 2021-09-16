using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserManagement.Models;

namespace UserManagement.Providers
{
    public class MenuAttribute : ActionFilterAttribute
    {
        public MenuAttribute()
        {

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var output = new List<NavigationMenuView>();
            var username = HttpContext.Current.User.Identity.Name;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["UserManagementDBEntities"].ConnectionString))
            {
                output = connection.Query<NavigationMenuView>("dbo.spr_GetPermissionsByUsername @EmailId", new { EmailId = username }).ToList();
            }
            filterContext.Controller.ViewBag.MainMenu = output;



            //base.OnActionExecuted(filterContext);
        }
    }
}