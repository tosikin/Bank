﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
   public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int proc) : base(sum, proc)
        {
        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт счет довостребования {this.Id}", this.Sum));
        }
    }
}
