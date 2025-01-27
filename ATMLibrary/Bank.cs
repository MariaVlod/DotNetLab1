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
        public delegate void BanksHandler(Bank sender, BanksEventArgs e);
        public event BankHandler Notifications;

        public string BankName { get; private set; }
        public string BankAddress { get; private set; }
        public int ListAtms { get; set; }

        public Bank(string bankName, string bankAddress, int listAtms)
        {
            BankName = bankName;
            BankAddress = bankAddress;
            ListAtms = listAtms;
        }

        public bool Authentication(string cardNumber, int cardPin, Account[] accounts, out int user)
        {
            user = -1;

            
            if (string.IsNullOrWhiteSpace(cardNumber) || cardPin <= 0)
            {
                Notifications?.Invoke("Неправильний номер картки або PIN-код.");
                return false;
            }

            for (int i = 0; i < Account.usercount; i++)
            {
                if (accounts[i].CardNumber == cardNumber)
                {
                    if (accounts[i].CardPin == cardPin)
                    {
                        user = i;
                        Notifications?.Invoke("Аутентифікація успішна!");
                        return true;
                    }
                    else
                    {
                        Notifications?.Invoke("Помилка введення даних! Повторіть спробу!");
                        return false;
                    }
                }
            }

            Notifications?.Invoke("Картка не знайдена.");
            return false;
        }
    }
}
