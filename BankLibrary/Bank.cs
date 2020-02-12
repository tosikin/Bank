using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    } 
    public class Bank<T> where T : Account
    {
        T[] accounts;
        public string Name { get; private set; }
        public Bank(string name)
        {
            this.Name = name;
        }
            public void Open(AccountType accountType, decimal sum, AccountStateHandler addSumHandler, 
                 AccountStateHandler withdrawSumHandler, AccountStateHandler calculationHandler,
                 AccountStateHandler cloceAcoountHandler, AccountStateHandler openStateHandler)
        {
            T newAccount = null;
            switch (accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 1) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 40) as T;
                    break;
            }

            if(newAccount == null)
            {
                throw new Exception("bad account");
            }
            else
            {
                if(accounts == null)
                {
                    accounts = new T[] { newAccount };
                }
                else
                {
                    T[] tempAccounts = new T[accounts.Length + 1];

                }
            }
        }

    }
}
