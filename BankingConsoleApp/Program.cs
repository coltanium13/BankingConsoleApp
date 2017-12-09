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
            Int32 option1 = DisplayMainMenu();
            MainCall(option1);
            Console.Read();
        }
        private static DataCache dataCacheInstance = DataCache.Instance;
        private static BankAccount activeAccount = new BankAccount();

        #region Console Main Menu
        private static int DisplayMainMenu()
        {
            Console.WriteLine("\nSelect any option:");
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
        private static void MainCall(Int32 option)
        {
            Int32 result = 0;
            Int32 empId1 = 0;
            switch (option)
            {
                case 1: //1. Select a Bank Account to Use
                    SelectAccount();
                    break;
                case 2: //2. Add Bank Account
                    //DisplayList();
                    Int32 option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 3: //3. Make a Deposite
                    Console.WriteLine("Enter EmployeeId which you want to update:");
                    empId1 = Convert.ToInt32(Console.ReadLine());
                    //result = update(empId1);
                    break;
                case 4: //4. Make a Withdrawl
                    Console.WriteLine("Enter EmployeeId which you want to delete:");
                    empId1 = Convert.ToInt32(Console.ReadLine());
                    //result = DeleteEmployee(empId1);
                    if (result == 1)
                        Console.WriteLine("Employee deleted");
                    else
                        Console.WriteLine("Employee with ID:" + empId1 + " not found");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
                case 5: //5. Exit
                    Console.WriteLine("BYEEEEEEEEEEEEEEEEEEEEE!!!!!!!!!!!!!!!!!!!");
                    break;
                default:
                    Console.WriteLine("Invalid Input!!!!!!Re-Enter");
                    option1 = DisplayMainMenu();
                    MainCall(option1);
                    break;
            }
        }
        #endregion

        #region Menu Methods
        private static void SelectAccount()
        {
            if (dataCacheInstance.GetBankAccounts(null).Count() <= 0)
            {
                Console.WriteLine("There are no Bank Accounts. Please Create a new Account.");
                DisplayMainMenu();
            }
            else
            {
                foreach (BankAccount account in dataCacheInstance.GetBankAccounts(null))
                {
                    Console.WriteLine($"Account Id: {account.Id}    Account Name: {account.Name}" );
                }
                Console.WriteLine("Type in the Account Number you wish to use...");
                int accountId = Convert.ToInt32(Console.ReadLine());

                BankAccount selectedAccount =  dataCacheInstance.GetBankAccounts(accountId).FirstOrDefault();
                activeAccount = selectedAccount;

                Console.WriteLine($"The Active Bank Account is {activeAccount.Name}");
                DisplayMainMenu();
            }
        }
        #endregion
    }
}
