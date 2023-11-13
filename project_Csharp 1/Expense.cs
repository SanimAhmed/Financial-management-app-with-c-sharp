using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        // Simulating a database context using a normal array
        private static Expense[] expenses = new Expense[10]; // Initial size of 10, can be adjusted
        private static int count = 0;

        // Add Expense
        public void AddExpense(int userId, decimal amount, string category, DateTime date, string notes)
        {
            if (count == expenses.Length)
            {
                IncreaseArraySize();
            }

            var expense = new Expense
            {
                ExpenseId = count + 1,
                UserId = userId,
                Amount = amount,
                Category = category,
                Date = date,
                Notes = notes
            };

            expenses[count] = expense;
            count++;
        }

        // Edit Expense
        public void EditExpense(int expenseId, decimal amount, string category, DateTime date, string notes)
        {
            for (int i = 0; i < count; i++)
            {
                if (expenses[i] != null && expenses[i].ExpenseId == expenseId)
                {
                    expenses[i].Amount = amount;
                    expenses[i].Category = category;
                    expenses[i].Date = date;
                    expenses[i].Notes = notes;
                    break;
                }
            }
        }

        // Delete Expense
        public void DeleteExpense(int expenseId)
        {
            for (int i = 0; i < count; i++)
            {
                if (expenses[i] != null && expenses[i].ExpenseId == expenseId)
                {
                    expenses[i] = null; // Set the expense to null to 'delete' it
                    ConsolidateArray(); // Consolidate the array to fill in the null gap
                    break;
                }
            }
        }
        public static Expense[] GetExpenseRecordsByUser(int userId)
        {
            return expenses.Where(expense => expense != null && expense.UserId == userId).ToArray();
        }
        public static decimal GetTotalExpensesByUser(int userId)
        {
            return expenses.Where(expense => expense != null && expense.UserId == userId).Sum(expense => expense.Amount);
        }
        // Method to increase the size of the array
        private void IncreaseArraySize()
        {
            Expense[] newExpenses = new Expense[expenses.Length * 2];
            expenses.CopyTo(newExpenses, 0);
            expenses = newExpenses;
        }

        public static decimal CalculateTotalExpensesByCategory(string category)
    {
        decimal total = 0;
        for (int i = 0; i < count; i++)
        {
            if (expenses[i] != null && expenses[i].Category == category)
            {
                total += expenses[i].Amount;
            }
        }
        return total;
    }

        // Method to consolidate the array after deletion
        private void ConsolidateArray()
        {
            Expense[] newExpenses = new Expense[expenses.Length];
            int newIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if (expenses[i] != null)
                {
                    newExpenses[newIndex++] = expenses[i];
                }
            }
            expenses = newExpenses;
            count = newIndex;
        }
    }

}
