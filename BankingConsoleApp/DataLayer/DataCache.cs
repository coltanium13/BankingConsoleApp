using BankingConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsoleApp.DataLayer
{
    public sealed class DataCache
    {
        
        private static readonly DataCache instance = new DataCache();

        private DataCache() { }

        //Singleton instance
        public static DataCache Instance
        {
            get
            {
                return instance;
            }
        }

        private List<BankAccount> bankAccountCache = new List<BankAccount>();

        #region BankAccount accessor Methods
        public List<BankAccount> GetBankAccounts(int? Id)
        {

            if (Id.HasValue)
                return bankAccountCache.Where(account => account.Id == Id).ToList();
            return bankAccountCache;
        }

        public bool AddBankAccount(BankAccount account)
        {
            bool success = false;
            if (!string.IsNullOrEmpty(account.Name))
            {
                account.Name = account.Name;
                account.Id = this.bankAccountCache.Count() > 0 ? this.bankAccountCache.Max(acct => acct.Id) + 1 : 1;

                this.bankAccountCache.Add(account);
                success = true;
            }
            else
            {
                success = false;
                Console.WriteLine("Bank Account Creation Failed.");
            }
            return success;
        }

        public decimal GetBalance(int accountId)
        {
            decimal balance = 0.0m;
            try
            {
                if (bankAccountCache.Count > 0)
                {
                    balance = bankAccountCache.Where(account => account.Id == accountId).Select(a => a.Balance).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not find balance for bank account with Id {accountId}");
            }
            return balance;
        }

        public bool Deposit(int accountId, decimal amount)
        {
            bool success = false;
            BankAccount account;
            try
            {
                if (bankAccountCache.Count > 0)
                {
                    account = bankAccountCache.Where(acct => acct.Id == accountId).FirstOrDefault();
                    account.Balance += amount;
                    account.TransactionLog.Add($"A deposit of {String.Format("{0:C}", amount)} was made at {DateTime.Now}");
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find balance for bank account with Id {accountId}");
            }
            return success;
        }

        public bool Withdrawl(int accountId, decimal withdrawlAmount)
        {
            bool success = false;
            BankAccount account;
            try
            {
                if (bankAccountCache.Count > 0)
                {
                     account = bankAccountCache.Where(acct => acct.Id == accountId).Select(a => a).FirstOrDefault();
                    if (account.Balance > withdrawlAmount)
                    {
                        account.Balance -= withdrawlAmount;
                        account.TransactionLog.Add($"A withdrawl of {String.Format("{0:C}", withdrawlAmount)} was made at {DateTime.Now}");
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find balance for bank account with Id {accountId}");
            }
            return success;
        }

        /// <summary>
        /// Returns the transaction log list for a bank account
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public List<string> GetTransactionLogHistory(int bankAccountId)
        {
            BankAccount account = bankAccountCache.Where(acct => acct.Id == bankAccountId).FirstOrDefault();
            return account.TransactionLog;
        }
        #endregion
    }
}
