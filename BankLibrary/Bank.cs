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
            
            if(accounts == null)
                {
                    accounts = new T[] { newAccount };
                }
            else
                {
                    T[] tempAccounts = new T[accounts.Length + 1];
                    for (int i = 0; i < accounts.Length; i++)
                        tempAccounts[i] = accounts[i];
                    tempAccounts[accounts.Length] = newAccount;
                    accounts = tempAccounts;
                }
                newAccount.Added += addSumHandler;
                newAccount.Calculated += calculationHandler;
                newAccount.Closed += cloceAcoountHandler;
                newAccount.Opened += openStateHandler;
                newAccount.Withdrawed += withdrawSumHandler;
                newAccount.Open();
        }

        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not found");
            account.Withdraw(sum);
        }

        public void Close(int id)
        {
            T account = FindAccount(id, out int index);
            if (account == null)
                throw new Exception("Account not found");
            account.Close();
            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                T[] tempAccounts = new T[accounts.Length - 1];
                for(int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }




        public T FindAccount(int id)
        {
            for(int i =0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        public T FindAccount(int id, out int index)
        {
            for(int i =0; i < accounts.Length; i++)
            {
                if(accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }
        

    }
}
