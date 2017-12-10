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

        public bool Deposite(int accountId, decimal amount)
        {
            bool success = false;
            try
            {
                if (bankAccountCache.Count > 0)
                {
                    decimal balance = bankAccountCache.Where(account => account.Id == accountId).Select(a => a.Balance += amount).FirstOrDefault();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find balance for bank account with Id{accountId}");
            }
            return success;
        }

        public bool Withdrawl(int accountId, decimal amount)
        {
            bool success = false;
            
            try
            {
                if (bankAccountCache.Count > 0)
                {
                    decimal balance = bankAccountCache.Where(account => account.Id == accountId).Select(a => a.Balance).FirstOrDefault();
                    balance -= amount;
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find balance for bank account with Id{accountId}");
            }
            return success;
        }
        #endregion
    }
}
