using System;
using System.Linq;

namespace ATMLibrary
{
    public class AutomatedTellerMachine
    {
        public delegate void BankOperationsHandler(AutomatedTellerMachine sender, BankOperationsEventArgs e);
        public event BankOperationsHandler Notify;

        public int ATM_ID { get; }
        public double ATM_Balance { get; private set; }
        public string ATM_Name { get; }

        public AutomatedTellerMachine(int atm_ID, double atm_Balance, string atm_Name)
        {
            ATM_ID = atm_ID;
            ATM_Balance = atm_Balance;
            ATM_Name = atm_Name;
        }

        private bool ValidateSum(double sum)
        {
            if (sum <= 0)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Сума повинна бути більшою за 0", sum));
                return false;
            }
            return true;
        }

        private bool HasSufficientFunds(double sum, double balance)
        {
            if (balance < sum)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs("Недостатньо коштів", sum));
                return false;
            }
            return true;
        }

        public void Deposit(double sum, Account account)
        {
            if (!ValidateSum(sum) || !HasSufficientFunds(sum, ATM_Balance)) return;

            account.Balance += sum;
            ATM_Balance -= sum;
            Notify?.Invoke(this, new BankOperationsEventArgs($"Зараховано {sum} UAH на рахунок", sum));
        }

        public void Withdraw(double sum, Account account)
        {
            if (!ValidateSum(sum) || !HasSufficientFunds(sum, account.Balance) || !HasSufficientFunds(sum, ATM_Balance)) return;

            account.Balance -= sum;
            ATM_Balance -= sum;
            Notify?.Invoke(this, new BankOperationsEventArgs($"Знято {sum} UAH з рахунку", sum));
        }

        public bool Transfer(double sum, string cardNumber, Account senderAccount, Account[] accounts)
        {
            if (!ValidateSum(sum) || !HasSufficientFunds(sum, senderAccount.Balance)) return false;

            var recipient = accounts.FirstOrDefault(acc => acc.CardNumber == cardNumber);
            if (recipient == null)
            {
                Notify?.Invoke(this, new BankOperationsEventArgs($"Картку {cardNumber} не знайдено", sum));
                return false;
            }

            senderAccount.Balance -= sum;
            recipient.Balance += sum;
            Notify?.Invoke(this, new BankOperationsEventArgs($"Перераховано {sum} UAH на картку {cardNumber}", sum));
            return true;
        }
    }
}
