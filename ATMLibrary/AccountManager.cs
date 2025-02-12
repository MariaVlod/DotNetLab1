using System.Linq;
using System;

namespace ATMLibrary
{
    public class AccountManager
    {
        private readonly Bank bank;

        private const string InsufficientFundsMessage = "Недостатньо коштів для переказу.";
        private const string NegativeBalanceMessage = "Initial balance cannot be negative.";

        public AccountManager(Bank bank)
        {
            this.bank = bank;
        }

        public void CreateAccount(string firstName, string lastName, double initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine(NegativeBalanceMessage);
                return;
            }

            var newAccount = new Account(firstName, lastName, initialBalance);
            bank.Accounts.Add(newAccount);
            Console.WriteLine($"Account created successfully for {firstName} {lastName} with account number {newAccount.AccountNumber}.");
        }

        public bool Authenticate(string cardNumber, int pinCode, out Account authenticatedAccount)
        {
            authenticatedAccount = bank.Accounts
                .FirstOrDefault(acc => acc.CardNumber == cardNumber && acc.CardPin == pinCode);

            return authenticatedAccount != null;
        }

        public void Withdraw(Account account, decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Сума для зняття не може бути від'ємною.");
                return;
            }

            account.Balance -= (double)amount;
        }

        public void Deposit(Account account, decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Сума для поповнення не може бути від'ємною.");
                return;
            }

            account.Balance += (double)amount;
        }

        public bool Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Сума переказу не може бути від'ємною.");
                return false;
            }

            if (fromAccount.Balance < (double)amount)
            {
                Console.WriteLine(InsufficientFundsMessage);
                return false;
            }

            Withdraw(fromAccount, amount);
            Deposit(toAccount, amount);
            return true;
        }
    }
}
