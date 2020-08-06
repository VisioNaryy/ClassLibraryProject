using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Added; //sum
        protected internal event AccountStateHandler Withdrawed; //sum
        protected internal event AccountStateHandler Opened; //account type
        protected internal event AccountStateHandler Closed; //account type
        protected internal event AccountStateHandler Calculated; //percentage
        protected internal event AccountStateHandler Displayed; //display more info about account

        protected static int counter = 0;
        protected int _days = 0;

        private decimal _sum;
        private decimal _percentage;

        public decimal Sum { get { return _sum; } private set { if (_sum >= 0) _sum = value; } }
        public decimal Percentage { get { return _percentage; } private set { if (_percentage >= 0) _percentage = value; } }
      
        public int Id { get; private set; }
        public Account(decimal sum):this(sum, 0)
        {
    
        }
        public Account(decimal sum, decimal percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        public virtual void Put(decimal sum)
        {
            if(sum>=0)
            {
                Sum += sum;
                OnAdded(new AccountEventArgs($"Added to the Account: {sum}", sum));
            }
            else
            {
                OnAdded(new AccountEventArgs($"Sum can't be negative!", 0));
            }
        }

        public virtual decimal Withdraw(decimal sum)
        {
            decimal res = 0;
            if (Sum>=sum)
            {
                Sum -= sum;
                res = sum;
                OnWithDrawed(new AccountEventArgs($"Withdrawed from the Account: {sum}", sum));
            }
            else
            {
                OnWithDrawed(new AccountEventArgs($"Warning. Not enough money in the Account with {Id} Id!", 0));
            }
            return res;
        }

        //operations with Account
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"New Account with Id: {Id} has been opened! Initial sum is {Sum}", Sum));
        }
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Account with Id: {Id} has been closed. Total sum is {Sum}", Sum));
        }
        protected internal void IncrementDays()
        {
            _days++;
        }
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum += increment;
            OnCalculated(new AccountEventArgs($"Interest of {increment} % was credited.", increment));

        }
        protected internal virtual void Display()
        {
            OnDisplayed(new AccountEventArgs($"State of the current Checking Account:\nAccountId: {Id}\tAccountSum: {Sum}",0));
        }


        //Invoking events
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        protected virtual void OnWithDrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        protected virtual void OnDisplayed(AccountEventArgs e)
        {
            CallEvent(e, Displayed);
        }

    }
}
