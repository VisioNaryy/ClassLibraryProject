using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class CheckingAccount: Account
    {
        public CheckingAccount(decimal sum):base(sum)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"New Checking Account with Id: {this.Id} has been opened. Initial sum is {this.Sum}", this.Sum));
        }
        protected internal override void Close()
        {
            base.OnOpened(new AccountEventArgs($"New Checking Account with Id: {this.Id} has been closed. Final sum is {this.Sum}", this.Sum));
        }
        public override void Put(decimal sum)
        {
            base.Put(sum);
        }
        public override decimal Withdraw(decimal sum)
        {
            return base.Withdraw(sum);
        }
        protected internal override void Display()
        {
            Console.WriteLine($"State of the current Checking Account:\nAccountId: {Id}\tAccountSum: {Sum}\tAccountDays: {_days}");
        }
    }
}
