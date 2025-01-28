using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public class TransactionManager
    {
        private readonly Bank bank;
        private readonly AccountManager accountManager;

        public TransactionManager(Bank bank, AccountManager accountManager)
        {
            this.bank = bank;
            this.accountManager = accountManager;
        }

        public bool TransferFunds(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            var fromAccount = bank.Accounts.FirstOrDefault(acc => acc.AccountNumber == fromAccountNumber);
            var toAccount = bank.Accounts.FirstOrDefault(acc => acc.AccountNumber == toAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("Одне або обидва рахунки не знайдено.");
                return false;
            }

            if ((decimal)fromAccount.Balance < amount)
            {
                Console.WriteLine("Недостатньо коштів.");
                return false;
            }

            
            accountManager.Withdraw(fromAccount, amount);
            accountManager.Deposit(toAccount, amount);

            Console.WriteLine($"Успішно переведено {amount:C} з рахунку {fromAccountNumber} на рахунок {toAccountNumber}.");
            return true;
        }
    }
}
