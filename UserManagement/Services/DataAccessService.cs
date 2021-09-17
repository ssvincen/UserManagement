using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement
{
    public class DataAccessService : IDataAccessService
    {
        private readonly IConnectionManager _connectionManager;
        public DataAccessService(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public bool ValidateUserLogin(string email, string password)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", dbType: DbType.String, value: email, direction: ParameterDirection.Input);
            param.Add("@Password", dbType: DbType.String, value: password, direction: ParameterDirection.Input);
            using (var db = _connectionManager.UserManagementDB())
            {
                return db.QueryFirstOrDefault<bool>("dbo.spr_ValidateUserLogin",
                    commandType: CommandType.StoredProcedure, param: param);
            }
        }


        public async Task<ResponseHelper> AddUserAsync(SignUpModel model)
        {
            var param = new DynamicParameters();
            param.Add("@FirstName", dbType: DbType.String, value: model.FirstName, direction: ParameterDirection.Input);
            param.Add("@Surname", dbType: DbType.String, value: model.Surname, direction: ParameterDirection.Input);
            param.Add("@EmailAddress", dbType: DbType.String, value: model.Email, direction: ParameterDirection.Input);
            param.Add("@PasswordHash", dbType: DbType.String, value: model.Password, direction: ParameterDirection.Input);
            param.Add("@EmailOTP", dbType: DbType.String, value: model.EmailOTP, direction: ParameterDirection.Input);
            param.Add("@GroupId", dbType: DbType.Int32, value: model.GroupId, direction: ParameterDirection.Input);

            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryFirstOrDefaultAsync<ResponseHelper>("dbo.spr_AddUser",
                    commandType: CommandType.StoredProcedure, param: param);
            }
        }

        public Task<bool> GetMenuItemsAsync(ClaimsPrincipal ctx, string ctrl, string act)
        {
            var param = new DynamicParameters();
            param.Add("", dbType: DbType.String, value: "", direction: ParameterDirection.Input);
            using (var db = _connectionManager.UserManagementDB())
            {

            }
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NavigationMenuView>> GetMenuItemsAsync(ClaimsPrincipal principal)
        {
            using (var db = _connectionManager.UserManagementDB())
            {

            }
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<NavigationMenuView>> GetPermissionsByGroupIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@GroupId", dbType: DbType.String, value: id, direction: ParameterDirection.Input);
            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryAsync<NavigationMenuView>("dbo.spr_GetPermissionByGroupId",
                    commandType: CommandType.StoredProcedure, param: param);
            }
            throw new System.NotImplementedException();
        }

        public Task<bool> SetPermissionsByGroupIdAsync(int id, IEnumerable<long> permissionIds)
        {
            using (var db = _connectionManager.UserManagementDB())
            {

            }
            throw new System.NotImplementedException();
        }

        public IEnumerable<NavigationMenuView> GetPermissionsByUsername(string email)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", dbType: DbType.String, value: email, direction: ParameterDirection.Input);
            using (var db = _connectionManager.UserManagementDB())
            {
                return db.Query<NavigationMenuView>("dbo.spr_GetPermissionsByUsername",
                    commandType: CommandType.StoredProcedure, param: param);
            }
        }

        public async Task<IEnumerable<NavigationMenuView>> GetPermissionsByUsernameAsync(string email)
        {
            var param = new DynamicParameters();
            param.Add("@EmailId", dbType: DbType.String, value: email, direction: ParameterDirection.Input);
            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryAsync<NavigationMenuView>("dbo.spr_GetPermissionsByUsername",
                    commandType: CommandType.StoredProcedure, param: param);
            }
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUserAsync()
        {
            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryAsync<UserViewModel>("dbo.spr_GetAllUsers",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ResponseHelper> AddGroupAsync(GroupModel model)
        {
            var param = new DynamicParameters();
            param.Add("@Name", dbType: DbType.String, value: model.Name, direction: ParameterDirection.Input);
            param.Add("@Description", dbType: DbType.String, value: model.Description, direction: ParameterDirection.Input);

            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryFirstOrDefaultAsync<ResponseHelper>("dbo.spr_AddGroup",
                    commandType: CommandType.StoredProcedure, param: param);
            }
        }

        public async Task<IEnumerable<GroupModelView>> GetAllActiveGroupsAsync()
        {
            using (var db = _connectionManager.UserManagementDB())
            {
                return await db.QueryAsync<GroupModelView>("dbo.spr_GetAllActiveGroups",
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}