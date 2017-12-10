using BankingConsoleApp.DataLayer;
using BankingConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int option1 = DisplayMainMenu();
            MainCall(option1);
            Console.Read();
        }
        private static DataCache dataCacheInstance = DataCache.Instance;
        private static BankAccount activeAccount = new BankAccount();

        #region Console Main Menu
        private static int DisplayMainMenu()
        {
            Console.WriteLine(string.Format("\nSelect any option:   Active Account: {0}", activeAccount == null ? "n/a" : activeAccount.Name));
            Console.WriteLine("1. Select a Bank Account to Use");
            Console.WriteLine("2. Add Bank Account");
            Console.WriteLine("3. Make a Deposite");
            Console.WriteLine("4. Make a Withdrawl");
            Console.WriteLine("5. Exit");

            Int32 option = 0;
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Some Error Occured");
            }
            return option;
        }
        #endregion

        #region Console Selection Switch
        private static void MainCall(int option)
        {
            int result = 0;
            int empId1 = 0;
            switch (option)
            {
                case 1: //1. Select a Bank Account to Use
                    SelectAccount();
                    break;
                case 2: //2. Add Bank Account
                    AddAccount();
                    break;
                case 3: //3. Make a Deposite
                    Deposite();
                    break;
                case 4: //4. Make a Withdrawl
                    Console.WriteLine("Enter EmployeeId which you want to delete:");
                    empId1 = Convert.ToInt32(Console.ReadLine());
                    //result = DeleteEmployee(empId1);
                    if (result == 1)
                        Console.WriteLine("Employee deleted");
                    else
                        Console.WriteLine("Employee with ID:" + empId1 + " not found");
                    //option1 = DisplayMainMenu();
                    //MainCall(option1);
                    break;
                case 5: //5. Exit
                    Console.WriteLine("Thanks for banking with Mo'Money Bank!");
                    break;
                default:
                    Console.WriteLine("Invalid Input! Re-Enter");
                    int option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
        }
        #endregion

        #region Menu Methods
        /// <summary>
        /// Displays a list of accounts and prompts for a selection of one of the accounts.
        /// </summary>
        private static void SelectAccount()
        {
            int accountId = 0;
            int option1 = 0;
            if (dataCacheInstance.GetBankAccounts(null).Count() <= 0)
            {
                Console.WriteLine("There are no Bank Accounts. Please Create a new Account.");
                option1 = DisplayMainMenu();
                MainCall(option1);
            }
            else
            {
                foreach (BankAccount account in dataCacheInstance.GetBankAccounts(null))
                {
                    Console.WriteLine($"Account Id: {account.Id}    Account Name: {account.Name}" );
                    Console.WriteLine("");
                }
                Console.WriteLine("Type in the Account Number you wish to use...");
                try
                {
                    accountId = Convert.ToInt32(Console.ReadLine());
                    switchToBankAccount(accountId);
                }
                catch
                {
                    Console.WriteLine("Invalid Account! Returning to Main Menu.");
                }
                option1 = DisplayMainMenu();
                MainCall(option1);
            }
        }

        /// <summary>
        /// Selects the bank account for a given account Id
        /// </summary>
        /// <param name="accountId"></param>
        private static void switchToBankAccount(int accountId)
        {
            BankAccount selectedAccount = dataCacheInstance.GetBankAccounts(accountId).FirstOrDefault();

            activeAccount = selectedAccount ?? null;
            if (activeAccount != null)
                Console.WriteLine($"The Active Bank Account is {activeAccount.Name}");
            else
            {
                Console.WriteLine($"The account Id {accountId} is invalid. Returning to Main menu.");
            }
        }

        private static void AddAccount()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter the Name you want to call the account");
            string accountName = Console.ReadLine();
            BankAccount newAccount = new BankAccount(accountName);
            if (dataCacheInstance.AddBankAccount(newAccount))
            {
                Console.Write($"You created the account called {newAccount.Name}");
                Console.WriteLine("");
                activeAccount = newAccount;
            }
            else
                Console.WriteLine("The Account creation failed.. Returning to main menu");
            Int32 option1 = DisplayMainMenu();
            MainCall(option1);
        }

        private static void Deposite()
        {
            Console.WriteLine("");
            if(activeAccount != null)
            {
                Console.WriteLine("Enter the amount you wish to deposite...");
                decimal depositeAmount = Convert.ToDecimal(Console.ReadLine());

                if (dataCacheInstance.Deposite(activeAccount.Id, depositeAmount))
                {
                    Console.WriteLine($"You deposited {depositeAmount}.");
                    Console.WriteLine($"The new balace for {activeAccount.Name} is ${activeAccount.Balance}");
                }
                else
                    Console.WriteLine("Something went wrong with the deposite...");
            }
            else
            {
                Console.WriteLine("Please select an account before depositing money. Returning to main menu.");
            }

            Int32 option1 = DisplayMainMenu();
            MainCall(option1);

        }

        private static void Withdrawl()
        {

        }
        #endregion
    }
}
