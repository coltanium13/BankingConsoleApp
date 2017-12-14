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
            dataCacheInstance.GetBankAccounts(null).RemoveAll(acct => acct != null);
        }

        [Test]
        public void AddTestBankAccountToCache()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            Assert.That(dataCacheInstance.GetBankAccounts(null).Count == 1);
        }

        [Test]
        public void GetBalanceForTestBankAccount()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            Assert.That(dataCacheInstance.GetBankAccounts(1).Select(acct => acct).First().Balance == 0.0m);
        }

        [Test]
        public void DepositToTestBankAccount()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            dataCacheInstance.Deposit(1, 50.90m);
            Assert.That(dataCacheInstance.GetBankAccounts(1).Select(acct => acct).First().Balance == 50.90m);
        }

        [Test]
        public void WithdrawlFromTestBankAccount()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            dataCacheInstance.Deposit(1, 100.0m);
            dataCacheInstance.Withdrawl(1, 60.20m);
            Assert.That(dataCacheInstance.GetBankAccounts(1).Select(acct => acct).First().Balance == 39.80m);
        }

        [Test]
        public void OverdraftWithdrawlFromTestBankAccount()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            dataCacheInstance.Deposit(1, 100.0m);
            Assert.That(dataCacheInstance.Withdrawl(1, 200.0m) == false);
        }

        [Test]
        public void GetTransactionHistoryForTestBankAccount()
        {
            dataCacheInstance.AddBankAccount(testBankAccount);
            Assert.That(dataCacheInstance.GetBankAccounts(1).Select(acct => acct).First().TransactionLog[0].Contains("Account Test Account was created"));
        }
    }
}
