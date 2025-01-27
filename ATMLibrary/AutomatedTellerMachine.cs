using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMLibrary
{
    public class AutomatedTellerMachine
    {
        public delegate void BankOperationsHandler(AutomatedTellerMachine sender, BankOperationsEventArgs e);

        public event BankOperationsHandler Notify;

        public int ATM_ID { get; protected set;}
        public double ATM_Balance { get; set;}
        public string ATM_Name { get; protected set;}


        public AutomatedTellerMachine(int atm_ID, double atm_Balance, string atm_Name) 
        { 
            ATM_ID = atm_ID;
            ATM_Balance = atm_Balance;
            ATM_Name = atm_Name;

        }

        public void Deposit(double sum, Account[] accounts, int user) 
        {
            if (sum <= 0)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Сума повинна бути більшою за 0", sum));
                return;
            }

            if (ATM_Balance < sum)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Технічні проблеми. Недостатньо готівки в банкоматі(.", sum));
            }

            if (accounts[user].Balance >= sum)
            {
                accounts[user].Balance += sum;
                ATM_Balance -= sum;
                Notify?.Invoke(this, new BankOperationsEventArgs($"Знято {sum} UAH з рахунку", sum));
            }

            else
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Недостатньо коштів на рахунку", sum));
            }
        }

        public void Withdraw(double sum, Account[] accounts, int user)
        {
            if (sum <= 0)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Сума повинна бути більшою за 0", sum));
                return;
            }

            if (ATM_Balance < sum)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Технічні проблеми. Недостатньо готівки в банкоматі.", sum));
                return;
            }

            if (accounts[user].Balance >= sum)
            {
                accounts[user].Balance -= sum;
                ATM_Balance -= sum;
                Notify?.Invoke(this, new BankOperationsEventArgs($"Знято {sum} UAH з рахунку", sum));
            }
            else
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Недостатньо коштів на рахунку", sum));
            }
        }




        public bool Transfer(double sum, string cardNumber, Account[] accounts, int user)
        {
            if (sum <= 0)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Сума повинна бути більшою за 0", sum));
                return false;
            }

            if (accounts[user].Balance < sum)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Недостатньо коштів на рахунку", sum));
                return false;
            }

            Account recipient = Array.Find(accounts, acc => acc.CardNumber == cardNumber);

            if (recipient != null)
            {
                accounts[user].Balance -= sum;
                recipient.Balance += sum;
                Notify?.Invoke(this, new BankOperationsEventArgs($"{sum} UAH було перераховано на картку {cardNumber}", sum));
                return true;
            }
            else
            {
                Notify?.Invoke(this, new BankOperationsEventArgs($"Картку {cardNumber} не знайдено", sum));
                return false;
            }
        }
    }

}
