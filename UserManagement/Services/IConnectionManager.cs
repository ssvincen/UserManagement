using System.Data;

namespace UserManagement.Services
{
    public interface IConnectionManager
    {
        IDbConnection UserManagementDB();
    }
}
