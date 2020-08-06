using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public enum AccountType
    {
        Checking,
        Deposit
    }
    public class Bank<T> where T:Account
    {
        List<T> accounts;

        public string Name { get; set; }
        public Bank(string name)
        {
            Name = name;
        }
        //creating an Account
        public void Open(AccountType accountType, decimal sum, AccountStateHandler addSumHandler,
            AccountStateHandler withdrawSumHandler, AccountStateHandler calculateHandler, AccountStateHandler openAccountHandler,
            AccountStateHandler closeAccountHandler, AccountStateHandler displayAccountHandler)
        {
            T newAccount = null;
            switch (accountType)
            {
                case AccountType.Checking:
                    newAccount = new CheckingAccount(sum) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 25) as T;
                    break;
            }
            if (newAccount == null)
                Console.WriteLine("Account creation error.");
            //add new Account to List<T>
            if (accounts == null)
                accounts = new List<T>() { newAccount};
            else
            {
                accounts.Add(newAccount);
            }

            //events handler
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Calculated += calculateHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Displayed += displayAccountHandler;
            if(accountType == AccountType.Checking)
            {
                newAccount.Calculated -= calculateHandler;
            }

            newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if(account == null)
                Console.WriteLine("Can not find an Account!");
            account.Put(sum);
        }
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if(account == null)
                Console.WriteLine("Can not find an Account!");
            account.Withdraw(sum);
        }
        public void Close(int id)
        {
            T account = FindAccount(id, out int index);
            if (account == null)
                Console.WriteLine("Can not find an Account!");
            account.Close();
            accounts.RemoveAt(index);
        }
        public void CalculatePercentage()
        {
            if (accounts == null)
                return;
            foreach (var acc in accounts)
            {
                acc.IncrementDays();
                acc.Calculate();
            }
        }
        public void Display(int id)
        {
            T account = FindAccount(id);
            if (account == null)
                Console.WriteLine("Can not find an Account!");
            account.Display();
        }
        public T FindAccount(int id)
        {
            foreach (var acc in accounts)
            {
                if (acc.Id == id)
                    return acc;
            }
            return null;
        }
        public T FindAccount(int id, out int index)
        {
            foreach (var acc in accounts)
            {
                if(acc.Id == id)
                {
                    index = accounts.IndexOf(acc);
                    return acc;
                }
            }
            index = -1;
            return null;
        }
    }
}
