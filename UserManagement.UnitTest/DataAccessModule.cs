using Ninject.Modules;

namespace UserManagement.UnitTest
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataAccessService>().To<DataAccessService>();
        }
    }
}
