using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public class BankOperationsEventArgs
    {
        public string Message { get; }
        public double Amount { get; }

        public BankOperationsEventArgs(string message, double amount)
        {
            Message = message;
            Amount = amount;
        }
    }
}
