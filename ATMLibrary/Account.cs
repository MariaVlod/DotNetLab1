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
        public static int usercount = 0;
        public string CardNumber { get; set; }
        public int CardPin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double Balance { get; set; }


        private AccountHandler info;
        public Account(string cardNumber, int cardPin, string firstName, string lastName, string email, string phoneNumber, double balance) 
        {
            CardNumber = cardNumber;
            CardPin = cardPin;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Balance = balance;
            usercount++;
        }
        public void RegisterHandler(AccountHandler del)
        {
            info = del;
        }

        public void PrintInfo()
        {
            info?.Invoke($"Ім'я: {FirstName}\nПрізвище: {LastName}\nНомер картки: {CardNumber}\nEmail: {Email}\nТелефон: {PhoneNumber}\nБаланс: {Balance}");
        }

        public void PrintBalance()
        {
            info?.Invoke($"Баланс: {Balance}");
        }

        public bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                info?.Invoke("Сума для зняття має бути більшою за нуль.");
                return false;
            }

            if (amount > Balance)
            {
                info?.Invoke("Недостатньо коштів на рахунку.");
                return false;
            }

            Balance -= amount;
            info?.Invoke($"Знято {amount}. Новий баланс: {Balance}");
            return true;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                info?.Invoke("Сума поповнення повинна бути бвльше за нуль.");
                return;
            }
            Balance += amount;
            info?.Invoke($"Ваш рахунок попвнено на {amount}. Новий баланс становить: {Balance}");
        }

    }
}
