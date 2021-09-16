using System.Web.Mvc;
using UserManagement.Providers;

namespace UserManagement
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MenuAttribute());
        }
    }
}
