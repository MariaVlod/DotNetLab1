using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public delegate void AccountHandler(string message);
    public class Account
    {
        public delegate void BankOperationsHandler(Account sender, BankOperationsEventArgs e);

        public static int UserCount = 0;

        public string AccountNumber { get; set; }
        public string AccountHolder => $"{FirstName} {LastName}";
        public string CardNumber { get; set; }
        public int CardPin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Balance { get; set; }

        private AccountHandler info;

        // Основний конструктор
        public Account(string cardNumber, int cardPin, string firstName, string lastName, string email, string phoneNumber, double balance)
        {
            CardNumber = cardNumber;
            CardPin = cardPin;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Balance = balance;

            AccountNumber = GenerateAccountNumber();
            UserCount++;
        }

        // Спрощений конструктор для мінімальних даних
        public Account(string firstName, string lastName, double balance)
        {
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;

            AccountNumber = GenerateAccountNumber();
            UserCount++;
        }

        // Генерація номера акаунта
        private string GenerateAccountNumber()
        {
            return $"ACC{DateTime.Now.Ticks}";
        }

        public void RegisterHandler(AccountHandler del)
        {
            info = del;
        }

        public void PrintInfo()
        {
            info?.Invoke($"Ім'я: {FirstName}\nПрізвище: {LastName}\nБаланс: {Balance}\nНомер акаунта: {AccountNumber}");
        }

        public void PrintBalance()
        {
            info?.Invoke($"Баланс: {Balance}");
        }

        public void Withdraw(Account account, decimal amount)
        {
            account.Balance -= (double)amount; // Кастуємо до double
        }

        public void Deposit(Account account, decimal amount)
        {
            account.Balance += (double)amount; // Кастуємо до double
        }    
    }
}
