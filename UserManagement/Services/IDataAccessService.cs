using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement
{
    public interface IDataAccessService
    {
        Task<IEnumerable<UserViewModel>> GetAllUserAsync();
        Task<ResponseHelper> AddUserAsync(SignUpModel model);
        bool ValidateUserLogin(string email, string password);
        Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act);
        Task<IEnumerable<NavigationMenuView>> GetMenuItemsAsync(ClaimsPrincipal principal);
        Task<IEnumerable<NavigationMenuView>> GetPermissionsByRoleIdAsync(string id);
        Task<IEnumerable<NavigationMenuView>> GetPermissionsByUsernameAsync(string email);
        IEnumerable<NavigationMenuView> GetPermissionsByUsername(string email);
        Task<bool> SetPermissionsByRoleIdAsync(string id, IEnumerable<long> permissionIds);
    }
}
