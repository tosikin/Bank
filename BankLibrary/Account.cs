using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        static int count = 0;
        protected int _days = 0;
        
        public Account (decimal sum, int proc)
        {
            Sum = sum;
            Proc = proc;
            Id = ++count;
        }

        public decimal Sum { get; private set; }
        public int Proc { get; private set; }
        public int Id { get; private set; }

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if(e != null)
            {
                handler?.Invoke(this, e);
            }
        }

        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void Oncalculate(AccountEventArgs e)
        {
            CallEvent(e, Calculated); ;
        }
        
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило " + sum, sum));
        }

        public virtual void Withdraw(decimal sum)
        {
            if(sum <= Sum)
            {
                Sum -= sum;
                OnWithdrawed(new AccountEventArgs("Со счета сняли " + sum, sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs("Не досточно денег на  счете ", 0));
            }
        }

        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт счет  {Id}", Sum));
        }

        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs("Счет {Id} закрыт итоговая сумма {Sum}", Sum));
        }
        protected internal void IncrementDays()
        {
            _days++;
        }
        protected internal virtual void Calculate()
        {
            decimal sum = Sum * Proc / 100;
            Oncalculate(new AccountEventArgs("начисленны проценты {sum}", sum));
        }
        
    }
}
