using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public delegate void BankHandler(string message);
    public class Bank
    {
        public string Name { get; set; }
        public string Address { get; set; } 
        public List<Account> Accounts { get; private set; }
        private readonly AccountManager accountManager;

        public Bank(string name, string address, int maxAccounts)
        {
            Name = name;
            Address = address; 
            Accounts = new List<Account>();
            accountManager = new AccountManager(this);
            Accounts.Add(new Account("1234567890", 1234, "John", "Doe", "john@example.com", "1234567890", 5000));
            Accounts.Add(new Account("0987654321", 4321, "Jane", "Doe", "jane@example.com", "0987654321", 10000));
        }

        public bool Authenticate(string cardNumber, int pinCode, out int userIndex)
        {
            return accountManager.Authenticate(cardNumber, pinCode, out userIndex);
        }

        public void CreateAccount(string accountHolder, decimal initialBalance)
        {
            var nameParts = accountHolder.Split(' ');
            if (nameParts.Length < 2)
            {
                Console.WriteLine("Error: Full name must include both first and last name.");
                return;
            }

            string firstName = nameParts[0];
            string lastName = nameParts[1];

            accountManager.CreateAccount(firstName, lastName, (double)initialBalance);
        }
    }
}
