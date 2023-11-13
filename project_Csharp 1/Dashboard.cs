using System;

namespace project_Csharp_1
{
    public class Dashboard
    {
        public int UserId { get; set; }
        private Expense[] expenses;
        private int expenseCount;

        public Dashboard(int userId)
        {
            this.UserId = userId;
            this.expenses = new Expense[10]; // Initial size of 10, can be adjusted
            this.expenseCount = 0;
        }

        public void DisplayOverview()
        {
            Console.WriteLine("Dashboard Overview for User " + UserId);
            Console.WriteLine("-----------------------------------");

            // Display user's total expenses
            decimal totalExpenses = CalculateTotalExpenses();

            Console.WriteLine("Total Expenses: $" + totalExpenses);

            // Additional dashboard overview statistics can be added here

            // Display upcoming bill reminders (if applicable)
            DisplayUpcomingBillReminders();
        }

        public void DisplayNavigationMenu()
        {
            Console.WriteLine("\nNavigation Menu");
            Console.WriteLine("---------------");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. Exit");

            // Handle user input for navigation
            int choice = GetUserChoice(1, 2);

            switch (choice)
            {
                case 1:
                    AddExpense();
                    break;
                case 2:
                    Console.WriteLine("Exiting Dashboard...");
                    break;
            }
        }

        private void AddExpense()
        {
            Console.WriteLine("\nAdd Expense");
            Console.WriteLine("------------");

            Console.Write("Enter Expense Category: ");
            string category = Console.ReadLine();

            Console.Write("Enter Expense Amount: $");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (expenseCount == expenses.Length)
                {
                    IncreaseArraySize();
                }

                var expense = new Expense
                {
                    UserId = UserId,
                    Amount = amount,
                    Category = category,
                    Date = DateTime.Now,
                    Notes = ""
                };

                expenses[expenseCount++] = expense;
                Console.WriteLine("Expense added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid amount entered. Expense not added.");
            }
        }

        private decimal CalculateTotalExpenses()
        {
            decimal total = 0;
            for (int i = 0; i < expenseCount; i++)
            {
                total += expenses[i].Amount;
            }
            return total;
        }

        private void DisplayUpcomingBillReminders()
        {
            // Implement the logic to display upcoming bill reminders here
        }

        private void IncreaseArraySize()
        {
            Expense[] newExpenses = new Expense[expenses.Length * 2];
            Array.Copy(expenses, newExpenses, expenses.Length);
            expenses = newExpenses;
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
