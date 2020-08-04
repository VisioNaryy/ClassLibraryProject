using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    //delegate
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        public string Message { get; private set; }
        public decimal Sum { get; private set; }
        public AccountEventArgs(string mes, decimal sum)
        {
            Message = mes;
            Sum = sum;
        }
        //public AccountEventArgs(string mes)
        //{
        //    Message = mes;
        //}
    }
}
