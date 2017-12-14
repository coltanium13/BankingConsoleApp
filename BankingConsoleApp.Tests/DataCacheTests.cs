using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BankingConsoleApp.Tests
{
    [TestFixture]
    public class DataCacheTests
    {
        #region test properties
        private static DataLayer.DataCache dataCacheInstance;
        BankingConsoleApp.Model.BankAccount testBankAccount;
        #endregion

        [SetUp]
        public void Init()
        {
            dataCacheInstance = DataLayer.DataCache.Instance;
            testBankAccount = new Model.BankAccount("Test Account", "password1");
        }

        [TearDown]
        public void Cleanup()
        {
            testBankAccount = null;
        }

        [Test]
        public void LoginToTestBankAccount()
        {

        }

    }
}
