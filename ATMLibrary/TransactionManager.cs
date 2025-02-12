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
            var fromAccount = FindAccount(fromAccountNumber);
            var toAccount = FindAccount(toAccountNumber);

            if (fromAccount == null || toAccount == null)
                return false;

            if (!HasSufficientFunds(fromAccount, amount))
                return false;

            PerformTransfer(fromAccount, toAccount, amount);

            return true;
        }

        private Account FindAccount(string accountNumber)
        {
            var account = bank.Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
            if (account == null)
                Console.WriteLine($"Рахунок {accountNumber} не знайдено.");
            return account;
        }

        private bool HasSufficientFunds(Account fromAccount, decimal amount)
        {
            if ((decimal)fromAccount.Balance < amount)
            {
                Console.WriteLine("Недостатньо коштів.");
                return false;
            }
            return true;
        }

        private void PerformTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            accountManager.Withdraw(fromAccount, amount);
            accountManager.Deposit(toAccount, amount);

            Console.WriteLine($"Успішно переведено {amount:C} з рахунку {fromAccount.AccountNumber} на рахунок {toAccount.AccountNumber}.");
        }
    }
}
