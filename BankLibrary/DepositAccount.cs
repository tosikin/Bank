using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public class DepositAccount: Account
    {
        public DepositAccount(decimal sum, int proc) : base(sum, proc)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($" Открыт депозит {this.Id}" , this.Sum));
        }
        public override void Put(decimal sum)
        {
            if(_days % 30 == 0)
            {
                base.Put(sum);
            }
            else
            {
                base.OnAdded(new AccountEventArgs("На счет можно положіть только после 30 дней",0));
            }
        }
        public override void Withdraw(decimal sum)
        {
            if(_days/20 > 1)
            {
                base.Withdraw(sum);
            }
            else
            {
                base.OnWithdrawed(new AccountEventArgs("нельзя снять деньги со счета", 0));
            }
        }
        protected internal override void Calculate()
        {
            if(_days %  30 == 0)
            {
                base.Calculate();
            }
        }
    }
}
