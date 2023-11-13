using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class TransactionHistory
    {
        private readonly int UserId;
        private Transaction[] transactions;
        private int transactionCount;

        public TransactionHistory(int userId)
        {
            UserId = userId;
            transactions = new Transaction[10]; // Initial size of 10, can be adjusted
            transactionCount = 0;
        }

        public void DisplayTransactionHistory()
        {
            Console.WriteLine("Transaction History for User " + UserId);
            Console.WriteLine("-----------------------------------");

            if (transactionCount == 0)
            {
                Console.WriteLine("No transaction records found.");
            }
            else
            {
                Console.WriteLine("Transaction Records:");
                foreach (var transaction in transactions.Take(transactionCount))
                {
                    Console.WriteLine($"Transaction ID: {transaction.TransactionId}, Amount: ${transaction.Amount}, Date: {transaction.Date.ToShortDateString()}, Description: {transaction.Description}");
                }
            }
        }

        public void AddTransaction(decimal amount, string description)
        {
            if (transactionCount == transactions.Length)
            {
                IncreaseArraySize();
            }

            var transaction = new Transaction
            {
                TransactionId = transactionCount + 1,
                UserId = UserId,
                Amount = amount,
                Date = DateTime.Now,
                Description = description
            };

            transactions[transactionCount++] = transaction;
            Console.WriteLine("Transaction added successfully.");
        }

        private void IncreaseArraySize()
        {
            Transaction[] newTransactions = new Transaction[transactions.Length * 2];
            Array.Copy(transactions, newTransactions, transactions.Length);
            transactions = newTransactions;
        }
    }
    }
