using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingConsoleApp.DataLayer;

namespace BankingConsoleApp.Model
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public List<string> TransactionLog { get; set; }

        public BankAccount() { }

        public BankAccount(string name, string password)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.Id = 0;
                this.Name = name;
                this.Password = password;
                this.Balance = 0.0m;
                this.TransactionLog = new List<string>();
                this.TransactionLog.Add($"Account {name} was created at {DateTime.Now}.");
            }
        }

        private DataCache dataCacheInstance = DataCache.Instance;

        public decimal GetBalance()
        {
            return this.dataCacheInstance.GetBalance(this.Id);
        }

        public bool Deposit(decimal amount)
        {
            return this.dataCacheInstance.Deposite(this.Id, amount);
        }

        public bool Withdrawl(decimal amount)
        {
            return this.dataCacheInstance.Withdrawl(this.Id, amount);
        }
    }
}
