using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public class BanksEventArgs
    {
        public string Message { get; }
        public double Amount { get; }

        public BanksEventArgs(string message, double amount)
        {
            Message = message;
            Amount = amount;
        }
    }
}
