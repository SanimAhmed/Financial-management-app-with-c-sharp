using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        private static Budget[] budgets = new Budget[10];
        private static int count = 0;

        public Budget(int userId, decimal amount, string category, DateTime startDate, DateTime endDate)
        {
            this.UserId = userId;
            this.Amount = amount;
            this.Category = category;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public static void AddBudget(Budget budget)
        {
            if (count == budgets.Length)
            {
                ResizeArray();
            }
            budget.BudgetId = count + 1;
            budgets[count++] = budget;
        }

        public static void EditBudget(int budgetId, decimal amount, DateTime startDate, DateTime endDate)
        {
            var budget = FindBudgetById(budgetId);
            if (budget != null)
            {
                budget.Amount = amount;
                budget.StartDate = startDate;
                budget.EndDate = endDate;
            }
        }

        public static void DeleteBudget(int budgetId)
        {
            for (int i = 0; i < count; i++)
            {
                if (budgets[i] != null && budgets[i].BudgetId == budgetId)
                {
                    budgets[i] = null;
                    ShiftArrayLeft(i);
                    count--;
                    break;
                }
            }
        }

        private static void ResizeArray()
        {
            Budget[] newArray = new Budget[budgets.Length * 2];
            Array.Copy(budgets, newArray, budgets.Length);
            budgets = newArray;
        }

        private static void ShiftArrayLeft(int startIndex)
        {
            for (int i = startIndex; i < count - 1; i++)
            {
                budgets[i] = budgets[i + 1];
            }
            budgets[count - 1] = null;
        }

        private static Budget FindBudgetById(int budgetId)
        {
            for (int i = 0; i < count; i++)
            {
                if (budgets[i] != null && budgets[i].BudgetId == budgetId)
                {
                    return budgets[i];
                }
            }
            return null;
        }

        // Additional methods to enhance functionality
        // Get Budgets by User
        public static Budget[] GetBudgetsByUser(int userId)
        {
            Budget[] userBudgets = new Budget[count];
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (budgets[i] != null && budgets[i].UserId == userId)
                {
                    userBudgets[index++] = budgets[i];
                }
            }
            return userBudgets;
        }

        // Calculate Remaining Budget
        public static decimal CalculateRemainingBudget(int budgetId)
        {
            var budget = FindBudgetById(budgetId);
            if (budget != null)
            {
                decimal totalExpenses = Expense.CalculateTotalExpensesByCategory(budget.Category);
                return budget.Amount - totalExpenses;
            }
            return 0;
        }

        // Check if Budget is Over
        public static bool IsBudgetOver(int budgetId)
        {
            var budget = FindBudgetById(budgetId);
            if (budget != null)
            {
                return CalculateRemainingBudget(budgetId) < 0;
            }
            return false;
        }

        // Export Budget Data (Example for CSV format)
        public static string ExportBudgetsToCSV()
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("BudgetId,UserId,Amount,Category,StartDate,EndDate");
            for (int i = 0; i < count; i++)
            {
                if (budgets[i] != null)
                {
                    var line = $"{budgets[i].BudgetId},{budgets[i].UserId},{budgets[i].Amount},\"{budgets[i].Category}\",{budgets[i].StartDate.ToShortDateString()},{budgets[i].EndDate.ToShortDateString()}";
                    csv.AppendLine(line);
                }
            }
            return csv.ToString();
        }

        // Additional functionalities and helper methods can be added as needed...
    }


}
