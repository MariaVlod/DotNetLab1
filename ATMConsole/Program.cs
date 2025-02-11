using ATMLibrary;
using System;

namespace ATMConsole
{
    public class Program
    {
        static Bank bank;
        static AutomatedTellerMachine atm;
        static Account[] accounts;
        static int currentUser = -1;

        static void Main(string[] args)
        {
            InitializeATM();
            ShowMainMenu();
        }


        static void InitializeATM()
        {
            bank = new Bank("BankName", "BankAddress", 10);
            atm = new AutomatedTellerMachine(1, 10000, "Some Address");
            accounts = new Account[]
            {
                new Account("1234567890", 1234, "John", "Doe", "john@example.com", "1234567890", 5000),
                new Account("0987654321", 4321, "Jane", "Doe", "jane@example.com", "0987654321", 10000)
            };
        }

        // Головне меню
        static void ShowMainMenu()
        {
            while (true)
            {
                DisplayMenuOptions();
                HandleUserChoice();
            }
        }

        static void DisplayMenuOptions()
        {
            Console.WriteLine("=== Банкомат ===");
            Console.WriteLine("1. Вхід (Аутентифікація)");
            Console.WriteLine("2. Перегляд балансу");
            Console.WriteLine("3. Зняття коштів");
            Console.WriteLine("4. Поповнення рахунку");
            Console.WriteLine("5. Переказ коштів");
            Console.WriteLine("6. Вихід");
        }


        static void HandleUserChoice()
        {
            Console.Write("Оберіть дію: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Authenticate(); break;
                case "2": CheckBalance(); break;
                case "3": Withdraw(); break;
                case "4": Deposit(); break;
                case "5": Transfer(); break;
                case "6": Environment.Exit(0); break;
                default: Console.WriteLine("Невірний вибір, спробуйте ще раз."); break;
            }
        }


        // Аутентифікація
        static void Authenticate()
        {
            Console.Write("Введіть номер картки: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Введіть PIN-код: ");
            if (int.TryParse(Console.ReadLine(), out int pinCode))
            {
                if (bank.Authenticate(cardNumber, pinCode, out currentUser))
                {
                    Console.WriteLine("Аутентифікація успішна!");
                }
                else
                {
                    Console.WriteLine("Невірний номер картки або PIN-код.");
                }
            }
            else
            {
                Console.WriteLine("Невірний формат PIN-коду.");
            }
        }

        // Перевірка входу
        static bool IsUserLoggedIn()
        {
            if (currentUser >= 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Спочатку потрібно виконати вхід.");
                return false;
            }
        }

        // Перегляд балансу
        static void CheckBalance()
        {
            if (!IsUserLoggedIn()) return;
            Console.WriteLine($"Ваш баланс: {accounts[currentUser].Balance} UAH");
        }

        // Зняття коштів
        static void Withdraw()
        {
            if (!IsUserLoggedIn()) return;

            Console.Write("Введіть суму для зняття: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                atm.Withdraw(amount, accounts, currentUser);
            }
            else
            {
                Console.WriteLine("Невірний формат суми.");
            }

        }

        // Поповнення рахунку
        static void Deposit()
        {
            if (!IsUserLoggedIn()) return;

            Console.Write("Введіть суму для поповнення: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                atm.Deposit(amount, accounts, currentUser);
            }
            else
            {
                Console.WriteLine("Невірний формат суми.");
            }
        }

        // Переказ коштів
        static void Transfer()
        {
            if (!IsUserLoggedIn()) return;

            Console.Write("Введіть номер картки отримувача: ");
            string recipientCardNumber = Console.ReadLine();

            Console.Write("Введіть суму для переказу: ");
            if (double.TryParse(Console.ReadLine(), out double amount))
            {
                if (atm.Transfer(amount, recipientCardNumber, accounts, currentUser))
                {
                    Console.WriteLine($"Переказано {amount} UAH на картку {recipientCardNumber}");
                }
                else
                {
                    Console.WriteLine("Помилка при переказі коштів.");
                }
            }
            else
            {
                Console.WriteLine("Невірний формат суми.");
            }
        }


    }

}
