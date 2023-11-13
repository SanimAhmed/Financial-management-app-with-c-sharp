using System;
using System.Collections.Generic;
using System.Linq;

public class ExpenseAnalysis
{
    public Dictionary<string, decimal> Expenses { get; private set; }

    public ExpenseAnalysis()
    {
        Expenses = new Dictionary<string, decimal>();
    }

    public void AddExpense(string category, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ArgumentException("Category cannot be null or whitespace.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        if (Expenses.ContainsKey(category))
        {
            Expenses[category] += amount;
        }
        else
        {
            Expenses.Add(category, amount);
        }
    }

    public void AnalyzeSpendingPatterns()
    {
        if (!Expenses.Any())
        {
            Console.WriteLine("No expenses recorded.");
            return;
        }

        foreach (var entry in Expenses)
        {
            Console.WriteLine($"Category: {entry.Key}, Amount: {entry.Value}");
        }

        // Example: Identifying the category with the highest expense
        var maxExpenseCategory = Expenses.OrderByDescending(entry => entry.Value).First();
        Console.WriteLine($"Highest Expense: {maxExpenseCategory.Key} - {maxExpenseCategory.Value}");
    }

    // Additional methods for detailed expense tracking and trend identification
}
