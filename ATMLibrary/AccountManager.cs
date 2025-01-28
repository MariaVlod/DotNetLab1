using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public class AccountManager
    {
        private readonly Bank bank;

        public AccountManager(Bank bank)
        {
            this.bank = bank;
        }

        public void CreateAccount(string firstName, string lastName, double initialBalance)
        {
            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative.");
                return;
            }

            var newAccount = new Account(firstName, lastName, initialBalance);
            bank.Accounts.Add(newAccount);
            Console.WriteLine($"Account created successfully for {firstName} {lastName} with account number {newAccount.AccountNumber}.");
        }

        
        public bool Authenticate(string cardNumber, int pinCode, out int userIndex)
        {
            userIndex = bank.Accounts.FindIndex(acc => acc.CardNumber == cardNumber && acc.CardPin == pinCode);
            return userIndex >= 0;
        }

        
        public void Withdraw(Account account, decimal amount)
        {
            account.Balance -= (double)amount;
        }

        public void Deposit(Account account, decimal amount)
        {
            account.Balance += (double)amount;
        }

        
        public bool Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if (fromAccount.Balance < (double)amount)
            {
                Console.WriteLine("Недостатньо коштів для переказу.");
                return false;
            }

            Withdraw(fromAccount, amount);
            Deposit(toAccount, amount);
            return true;
        }
    }
}
