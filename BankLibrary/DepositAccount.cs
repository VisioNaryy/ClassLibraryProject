using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DepositAccount:Account
    {
        public DepositAccount(decimal sum, decimal percentage):base(sum, percentage)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"New Deposit Account with Id: {this.Id} has been opened. Initial sum is {this.Sum}", this.Sum));
        }
        protected internal override void Close()
        {
            base.OnOpened(new AccountEventArgs($"New Deposit Account with Id: {this.Id} has been closed. Final sum is {this.Sum}", this.Sum));
        }
        public override void Put(decimal sum)
        {
            if (_days % 30 == 0)
                base.Put(sum);
            else base.OnAdded(new AccountEventArgs($"You can only put funds into your Deposit Account after a 30-day period!", 0));
        }
        public override decimal Withdraw(decimal sum)
        {
            if (_days % 30 == 0)
                return base.Withdraw(sum);
            else base.OnWithDrawed(new AccountEventArgs($"You can only withdraw funds from your Deposit Account after a 30-day period!", 0));
            return 0;
        }
        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
                base.Calculate();
            //else base.OnCalculated(new AccountEventArgs($"You can only credit interest on the Deposit Account after a 30-day period!", 0));
        }
        protected internal override void Display()
        {
            Console.WriteLine($"State of the current Account:\nAccountId: {Id}\tAccountSum: {Sum}\tAccountPercentage: {Percentage}\tAccountDays: {_days}");
        }
    }
}
