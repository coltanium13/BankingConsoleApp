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
            Console.WriteLine(string.Format("\nSelect any option:   Active Account: {0}", activeAccount.Name == null ? "n/a" : activeAccount.Name));
            Console.WriteLine("1. Select a Bank Account to Use");
            Console.WriteLine("2. Add Bank Account");
            Console.WriteLine("3. Make a Deposit");
            Console.WriteLine("4. Make a Withdrawl");
            Console.WriteLine("5. Check Balance");
            Console.WriteLine("6. View Transaction History");
            Console.WriteLine("7. Exit");

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
            switch (option)
            {
                case 1: //1. Select a Bank Account to Use
                    SelectAccount();
                    break;
                case 2: //2. Add Bank Account
                    AddAccount();
                    break;
                case 3: //3. Make a Deposit
                    Deposit();
                    break;
                case 4: //4. Make a Withdrawl
                    Withdrawl();
                    break;
                case 5: //Check Balance
                    CheckBalance();
                    break;
                case 6: //View Transaction Log
                    ViewTransactionLog();
                    break;
                case 7: //Exit
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
                    Console.WriteLine($"Account Number: {account.Id}    Account Name: {account.Name}" );
                    Console.WriteLine("");
                }
                Console.WriteLine("Type in the Account Number you wish to use...");
                try
                {
                    accountId = Convert.ToInt32(Console.ReadLine());
                    switchToBankAccount(accountId);
                }
                catch(Exception ex)
                {
                    //they entered something other than an int. Account number must be an int
                    Console.WriteLine($"Invalid Account Number: {ex.Message}");
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
            bool validated = false;

            //verify the password for the account trying to be selected
            if (selectedAccount != null)
            {
                Console.WriteLine($"Enter the password for {selectedAccount.Name}...");
                string password = Console.ReadLine();
                validated = validatePassword(selectedAccount, password);
                if (validated)
                {
                    Console.WriteLine($"The Active Bank Account is {activeAccount.Name}");
                    activeAccount = selectedAccount;
                }
                else
                {
                    Console.WriteLine($"The password for account {selectedAccount.Name} was Invalid. Returning to Main menu.");
                }
            }
            //The account Id trying to be selected was not found.
            else
            {
                Console.WriteLine($"The account number {accountId} was not found. Returning to Main menu.");
            }
        }

        /// <summary>
        /// Validate the password for a bank account.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool validatePassword(BankAccount account, string password)
        {
            bool valid = false;
            if (account.Password == password)
                valid = true;
            else
                valid = false;
            return valid;
        }

        private static void AddAccount()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter the Name you want to call the account");
            string accountName = Console.ReadLine();
            Console.WriteLine("Enter a password for the account");
            string password = Console.ReadLine(); ;
            //TODO: Add prompt to enter password for account.
            BankAccount newAccount = new BankAccount(accountName, password);
            if (dataCacheInstance.AddBankAccount(newAccount))
            {
                Console.Write($"You created the account called {newAccount.Name}");
                Console.WriteLine("");
                activeAccount = newAccount;
            }
            else
                Console.WriteLine("The Account creation failed.. Returning to main menu");
            int option1 = DisplayMainMenu();
            MainCall(option1);
        }

        private static void Deposit()
        {
            decimal depositAmount;
            Console.WriteLine("");
            if(activeAccount != null)
            {
                Console.WriteLine("Enter the amount you wish to deposit...");
                try
                {
                    depositAmount = Convert.ToDecimal(Console.ReadLine());

                    if (dataCacheInstance.Deposit(activeAccount.Id, depositAmount))
                    {
                        Console.WriteLine($"You deposited {depositAmount}.");
                        Console.WriteLine($"The new balace for {activeAccount.Name} is {String.Format("{0:C}", activeAccount.Balance)}");
                    }
                    else
                        Console.WriteLine("Something went wrong with the deposit...");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Enter a correct deposit amount: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Please select an account before depositing money. Returning to main menu.");
            }

            int option1 = DisplayMainMenu();
            MainCall(option1);

        }

        private static void Withdrawl()
        {
            Console.WriteLine("");
            decimal withdrawlAmount;
            if (activeAccount != null)
            {
                Console.WriteLine("Enter the amount you wish to withdrawl...");
                try
                {
                    withdrawlAmount = Convert.ToDecimal(Console.ReadLine());

                    if (dataCacheInstance.Withdrawl(activeAccount.Id, withdrawlAmount))
                    {
                        Console.WriteLine($"You withdrew {withdrawlAmount}.");
                        Console.WriteLine($"The new balace for {activeAccount.Name} is {String.Format("{0:C}", activeAccount.Balance)}");
                    }
                    else
                        Console.WriteLine($"You can not overdraft... sorry brah");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Enter a correct withdrawl amount: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Please select an account before withdrawling money. Returning to main menu.");
            }

            int option1 = DisplayMainMenu();
            MainCall(option1);
        }

        /// <summary>
        /// Shows the balance for the active account.
        /// </summary>
        public static void CheckBalance()
        {
            if(activeAccount.Name != null)
            {
                Console.WriteLine($"Your current balance for account {activeAccount.Name} is {String.Format("{0:C}", activeAccount.Balance)}");
            }
            else
            {
                Console.WriteLine("Please create an account. Returning to Main menu.");
            }

            int option1 = DisplayMainMenu();
            MainCall(option1);
        }

        /// <summary>
        /// Shows the list of transaction log history for the active account
        /// </summary>
        public static void ViewTransactionLog()
        {
            if (activeAccount.Name != null)
            {
                Console.WriteLine($"Transaction History for account {activeAccount.Name}: ");
                foreach(string log in activeAccount.TransactionLog) { Console.WriteLine(log); };
            }
            else
            {
                Console.WriteLine("Please create an account. Returning to Main menu.");
            }

            int option1 = DisplayMainMenu();
            MainCall(option1);
        }
        #endregion
    }
}
