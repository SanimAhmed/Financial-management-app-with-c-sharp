using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace project_Csharp_1
{
    

    public class ReportsAndAnalytics
    {
        private readonly int UserId;
        private readonly IncomeManagement IncomeManager; // Corrected type
        private readonly Expense ExpenseManager; // Ensure this is correct
        private readonly FinancialGoalManager GoalManager;

        public ReportsAndAnalytics(int userId, IncomeManagement incomeManager, Expense expenseManager, FinancialGoalManager goalManager)
        {
            UserId = userId;
            IncomeManager = incomeManager;
            ExpenseManager = expenseManager;
            GoalManager = goalManager;
        }

        public void DisplayIncomeReport()
        {
            Console.WriteLine("Income Report for User " + UserId);
            Console.WriteLine("-----------------------------------");

            var incomeRecords = IncomeManager.GetIncomeRecordsByUser();

            if (incomeRecords.Length == 0)
            {
                Console.WriteLine("No income records found.");
            }
            else
            {
                Console.WriteLine("Income Records:");
                foreach (var record in incomeRecords)
                {
                    // Updated to reflect the properties available in the Income class
                    Console.WriteLine($"Source: {record.Source}, Amount: ${record.Amount}, Date: {record.Date.ToShortDateString()}");
                }

                decimal totalIncome = incomeRecords.Sum(record => record.Amount);
                Console.WriteLine($"Total Income: ${totalIncome}");
            }
        }


        public void DisplayExpenseReport()
        {
            Console.WriteLine("Expense Report for User " + UserId);
            Console.WriteLine("-----------------------------------");

            var expenseRecords = Expense.GetExpenseRecordsByUser(UserId);

            if (expenseRecords.Length == 0)
            {
                Console.WriteLine("No expense records found.");
            }
            else
            {
                Console.WriteLine("Expense Records:");
                foreach (var record in expenseRecords)
                {
                    Console.WriteLine($"Expense ID: {record.ExpenseId}, Category: {record.Category}, Amount: ${record.Amount}, Date: {record.Date.ToShortDateString()}");
                }

                decimal totalExpenses = expenseRecords.Sum(record => record.Amount);
                Console.WriteLine($"Total Expenses: ${totalExpenses}");
            }
        }

        public void DisplayFinancialGoalProgress()
        {
            Console.WriteLine("Financial Goal Progress for User " + UserId);
            Console.WriteLine("-----------------------------------");

            var goals = GoalManager.GetFinancialGoalsByUser(UserId);

            if (goals.Length == 0)
            {
                Console.WriteLine("No financial goals found.");
            }
            else
            {
                Console.WriteLine("Financial Goals:");
                foreach (var goal in goals)
                {
                    decimal progressPercentage = (goal.CurrentAmount / goal.TargetAmount) * 100;
                    string status = goal.IsCompleted ? "Completed" : "In Progress";

                    Console.WriteLine($"Goal ID: {goal.GoalId}, Name: {goal.Name}, Target Amount: ${goal.TargetAmount}, Current Amount: ${goal.CurrentAmount}, Target Date: {goal.TargetDate.ToShortDateString()}, Status: {status}, Progress: {progressPercentage}%");
                }
            }
        }

        public void GenerateAnalytics()
        {
            Console.WriteLine("Generating Financial Analytics for User " + UserId);
            Console.WriteLine("-----------------------------------");

            decimal totalIncome = IncomeManager.GetTotalIncomeByUser();

            decimal totalExpenses = Expense.GetTotalExpensesByUser(UserId);
            decimal savingsRate = CalculateSavingsRate(totalIncome, totalExpenses);

            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expenses: ${totalExpenses}");
            Console.WriteLine($"Savings Rate: {savingsRate}%");

            var expenseRecords = Expense.GetExpenseRecordsByUser(UserId);
            GenerateExpenseCategoryBreakdown(expenseRecords);

            Console.WriteLine("\nGenerating Monthly Expense Report...");
            GenerateMonthlyExpenseReport(DateTime.Now.Year, DateTime.Now.Month);
        }

        private decimal CalculateSavingsRate(decimal totalIncome, decimal totalExpenses)
        {
            if (totalIncome == 0)
            {
                return 0; // Avoid division by zero
            }

            decimal savings = totalIncome - totalExpenses;
            decimal savingsRate = (savings / totalIncome) * 100;

            return savingsRate;
        }

        private void GenerateExpenseCategoryBreakdown(IEnumerable<Expense> expenseRecords)
        {
            Console.WriteLine("\nExpense Category Breakdown:");
            Console.WriteLine("---------------------------");

            var categoryGroups = expenseRecords.GroupBy(record => record.Category);

            foreach (var categoryGroup in categoryGroups)
            {
                string category = categoryGroup.Key;
                decimal totalExpense = categoryGroup.Sum(record => record.Amount);

                Console.WriteLine($"{category}: ${totalExpense}");
            }
        }

        public void GenerateMonthlyExpenseReport(int year, int month)
        {
            Console.WriteLine($"\nMonthly Expense Report for {GetMonthName(month)} {year}");
            Console.WriteLine("-----------------------------------");

            var expenseRecords = Expense.GetExpenseRecordsByUser(UserId);
            var filteredExpenses = expenseRecords.Where(record => record.Date.Year == year && record.Date.Month == month);

            decimal totalMonthlyExpense = filteredExpenses.Sum(record => record.Amount);

            Console.WriteLine($"Total Monthly Expense: ${totalMonthlyExpense}");

            foreach (var expense in filteredExpenses)
            {
                Console.WriteLine($"{expense.Date.ToShortDateString()} - {expense.Category}: ${expense.Amount}");
            }
        }

        private string GetMonthName(int month)
        {
            return new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM");
        }
    }

}
