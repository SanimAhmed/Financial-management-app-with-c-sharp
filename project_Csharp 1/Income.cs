using System;
using System.Linq;

namespace project_Csharp_1
{
    public class Income
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
    }

    public class IncomeManagement
    {
        private readonly int UserId;
        private Income[] incomes;
        private int incomeCount;

        public IncomeManagement(int userId)
        {
            UserId = userId;
            incomes = new Income[10]; // Initial size of 10, can be adjusted
            incomeCount = 0;
        }

        public void DisplayIncomeOverview()
        {
            Console.WriteLine("Income Overview for User " + UserId);
            Console.WriteLine("-----------------------------------");

            // Display user's total income
            decimal totalIncome = CalculateTotalIncome();

            Console.WriteLine("Total Income: $" + totalIncome);

            // Additional income overview statistics can be added here
        }

        public void DisplayIncomeMenu()
        {
            Console.WriteLine("\nIncome Menu");
            Console.WriteLine("---------------");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. View Income History");
            Console.WriteLine("3. Exit");

            // Handle user input for income management
            int choice = GetUserChoice(1, 3);

            switch (choice)
            {
                case 1:
                    AddIncome();
                    break;
                case 2:
                    ViewIncomeHistory();
                    break;
                case 3:
                    Console.WriteLine("Exiting Income Management...");
                    break;
            }
        }

        private void AddIncome()
        {
            Console.WriteLine("\nAdd Income");
            Console.WriteLine("------------");

            Console.Write("Enter Income Source: ");
            string source = Console.ReadLine();

            Console.Write("Enter Income Amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (incomeCount == incomes.Length)
                {
                    IncreaseArraySize();
                }

                var income = new Income
                {
                    UserId = UserId,
                    Amount = amount,
                    Source = source,
                    Date = DateTime.Now
                };

                incomes[incomeCount++] = income;
                Console.WriteLine("Income added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid amount entered. Income not added.");
            }
        }

        public Income[] GetIncomeRecordsByUser()
        {
            return incomes.Where(income => income != null && income.UserId == this.UserId).ToArray();
        }

        public decimal GetTotalIncomeByUser()
        {
            return incomes.Where(income => income != null && income.UserId == this.UserId).Sum(income => income.Amount);
        }

        private void ViewIncomeHistory()
        {
            Console.WriteLine("\nIncome History");
            Console.WriteLine("---------------");

            if (incomeCount == 0)
            {
                Console.WriteLine("No income records found.");
            }
            else
            {
                Console.WriteLine("Income Records:");
                foreach (var income in incomes.Take(incomeCount))
                {
                    Console.WriteLine($"Source: {income.Source}, Amount: ${income.Amount}, Date: {income.Date.ToShortDateString()}");
                }
            }
        }

        private decimal CalculateTotalIncome()
        {
            decimal total = 0;
            for (int i = 0; i < incomeCount; i++)
            {
                total += incomes[i].Amount;
            }
            return total;
        }

        private void IncreaseArraySize()
        {
            Income[] newIncomes = new Income[incomes.Length * 2];
            Array.Copy(incomes, newIncomes, incomes.Length);
            incomes = newIncomes;
        }

        private int GetUserChoice(int minChoice, int maxChoice)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minChoice && choice <= maxChoice)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid choice.");
                }
            }
        }
    }
}
