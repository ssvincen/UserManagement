using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Helper
{
    public class Data
    {
        private readonly IDataAccessService _dataAccessService;

        public Data(IDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }


        public IEnumerable<NavigationMenuView> GetMenues(string username)
        {
            return _dataAccessService.GetPermissionsByUsername(username);
        }
    }
}