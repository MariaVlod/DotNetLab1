using ATMLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMForms
{
    public partial class Form1 : Form
    {
        private Bank bank;
        private AutomatedTellerMachine atm;
        private Account[] accounts;
        private AccountManager accountManager;
        private int currentUser;
        public Form1()
        {
            InitializeComponent();
            InitializeATM();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeATM()
        {
            bank = new Bank("BankName", "BankAddress", 10);
            accountManager = new AccountManager(bank);
            atm = new AutomatedTellerMachine(1, 10000, "Some Address");
            accounts = new Account[]
            {
                new Account("1234567890", 1234, "John", "Doe", "john@example.com", "1234567890", 5000),
                new Account("0987654321", 4321, "Jane", "Doe", "jane@example.com", "0987654321", 10000)
            };
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string cardNumber = txtCardNumber.Text;
            int pinCode;

            if (int.TryParse(txtPinCode.Text, out pinCode))
            {
                if (accountManager.Authenticate(cardNumber, pinCode, out currentUser))
                {
                    MessageBox.Show("Вхід успішний!");
                }
                else
                {
                    MessageBox.Show("Неправильний номер картки або PIN-код.");
                }
            }
            else
            {
                MessageBox.Show("Невірний формат PIN-коду.");
            }
        }

        private void btnCheckBalance_Click(object sender, EventArgs e)
        {
            if (currentUser >= 0)
            {
                double balance = accounts[currentUser].Balance;
                MessageBox.Show($"Ваш баланс: {balance} UAH");
            }
            else
            {
                MessageBox.Show("Спочатку потрібно виконати вхід.");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (currentUser >= 0)
            {
                double amount;
                if (double.TryParse(txtAmount.Text, out amount))
                {
                    atm.Withdraw(amount, accounts, currentUser);
                }
                else
                {
                    MessageBox.Show("Невірний формат суми.");
                }
            }
            else
            {
                MessageBox.Show("Спочатку потрібно виконати вхід.");
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (currentUser >= 0)
            {
                double amount;
                if (double.TryParse(txtAmount.Text, out amount))
                {
                    atm.Deposit(amount, accounts, currentUser);
                }
                else
                {
                    MessageBox.Show("Невірний формат суми.");
                }
            }
            else
            {
                MessageBox.Show("Спочатку потрібно виконати вхід.");
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (currentUser >= 0)
            {
                double amount;
                string recipientCardNumber = txtRecipientCardNumber.Text;
                if (double.TryParse(txtAmount.Text, out amount))
                {
                    if (atm.Transfer(amount, recipientCardNumber, accounts, currentUser))
                    {
                        MessageBox.Show($"Перераховано {amount} UAH на картку {recipientCardNumber}");
                    }
                    else
                    {
                        MessageBox.Show("Помилка при перерахуванні коштів.");
                    }
                }
                else
                {
                    MessageBox.Show("Невірний формат суми.");
                }
            }
            else
            {
                MessageBox.Show("Спочатку потрібно виконати вхід.");
            }
        }
    }
    
}
