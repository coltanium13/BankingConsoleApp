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
        public decimal Balance { get; set; }

        public BankAccount() { }

        public BankAccount(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                this.Id = 0;
                this.Name = name;
                this.Balance = 0.0m;
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
