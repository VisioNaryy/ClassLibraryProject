using System;
using BankLibrary;

namespace ClassLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("DNB ASA");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1. Open an account\t 2. Withdraw funds from the account\t 3. Add funds to the account");
                Console.WriteLine("4. Close an account\t 5. Skip the day\t 6. Exit the program\t 7.Display more info");
                Console.WriteLine("Choose the number of an option: ");
                Console.ForegroundColor = color;

                try
                {
                    int command = Int32.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                        case 7:
                            DisplayAccount(bank);
                            break;
                            
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter the initial amount of funds to create an account:");
            decimal sum = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Choose an account type: 1. Checking 2. Deposit");

            AccountType accountType;
            int type = Int32.Parse(Console.ReadLine());
            if (type == 1)
                accountType = AccountType.Checking;
            else
                accountType = AccountType.Deposit;

            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler,
                (o, e) => Console.WriteLine(e.Message),
                CloseAccountHandler, OpenAccountHandler, DisplayAccountHandler);
        }
        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Enter the withdrawal sum:");
            decimal sum = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter the account Id:");
            int id = Int32.Parse(Console.ReadLine());

            bank.Withdraw(sum, id);
        }
        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Enter the sum to be put into the account:");
            decimal sum = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter the account Id:");
            int id = Int32.Parse(Console.ReadLine());

            bank.Put(sum, id);
        }
        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter the id of the account you want to close:");
            int id = Int32.Parse(Console.ReadLine());

            bank.Close(id);
        }
        private static void DisplayAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter the id of the account you want to display:");
            int id = Int32.Parse(Console.ReadLine());
            bank.Display(id);
        }

        //events handlers
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void DisplayAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
