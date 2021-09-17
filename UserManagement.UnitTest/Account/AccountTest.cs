using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace UserManagement.UnitTest.Account
{
    [TestClass]
    public class AccountTest
    {
        private readonly IDataAccessService _dataAccess;


        public AccountTest()
        {
            using (IKernel kernel = new StandardKernel(new DataAccessModule()))
            {
                _dataAccess = kernel.Get<IDataAccessService>();
            }
        }



        [TestMethod]
        public void ValidateUserLoginDetails()
        {
            bool isValidUser = _dataAccess.ValidateUserLogin("ss.vincen@gmail.com", Crypto.Hash("Password"));
            Assert.AreEqual(true, isValidUser);
        }
    }
}
