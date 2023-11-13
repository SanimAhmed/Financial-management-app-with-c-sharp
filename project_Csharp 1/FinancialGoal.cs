using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
    public class FinancialGoal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }

        public FinancialGoal(int userId, string name, decimal targetAmount, DateTime targetDate)
        {
            UserId = userId;
            Name = name;
            TargetAmount = targetAmount;
            CurrentAmount = 0; // Initialize current amount to zero
            TargetDate = targetDate;
            IsCompleted = false; // A goal is initially not completed
        }

        // Method to update the current amount saved towards the goal
        public void UpdateCurrentAmount(decimal amount)
        {
            CurrentAmount += amount;
            if (CurrentAmount >= TargetAmount)
            {
                IsCompleted = true;
            }
        }
    }


    public class FinancialGoalManager
    {
        private readonly int UserId;
        private FinancialGoal[] goals;
        private int goalCount;

        public FinancialGoalManager(int userId)
        {
            UserId = userId;
            goals = new FinancialGoal[10]; // Initial size of 10, can be adjusted
            goalCount = 0;
        }

        public void DisplayFinancialGoals()
        {
            Console.WriteLine("Financial Goals for User " + UserId);
            Console.WriteLine("-----------------------------------");

            if (goalCount == 0)
            {
                Console.WriteLine("No financial goals set.");
            }
            else
            {
                Console.WriteLine("Financial Goals:");
                foreach (var goal in goals.Take(goalCount))
                {
                    Console.WriteLine($"Goal ID: {goal.GoalId}, Name: {goal.Name}, Target Amount: ${goal.TargetAmount}, Current Amount: ${goal.CurrentAmount}, Target Date: {goal.TargetDate.ToShortDateString()}, Status: {(goal.IsCompleted ? "Completed" : "In Progress")}");
                }
            }
        }

        public void AddFinancialGoal(string name, decimal targetAmount, DateTime targetDate)
        {
            if (goalCount == goals.Length)
            {
                IncreaseArraySize();
            }

            var goal = new FinancialGoal(UserId, name, targetAmount, targetDate)
            {
                GoalId = goalCount + 1
            };

            goals[goalCount++] = goal;
            Console.WriteLine("Financial goal added successfully.");
        }

        public void UpdateGoalProgress(int goalId, decimal amount)
        {
            var goal = goals.FirstOrDefault(g => g.GoalId == goalId);
            if (goal != null && !goal.IsCompleted)
            {
                goal.UpdateCurrentAmount(amount);
                Console.WriteLine($"Progress updated for goal '{goal.Name}'. Current amount: ${goal.CurrentAmount}");
            }
            else
            {
                Console.WriteLine("Goal not found or already completed.");
            }
        }
        public FinancialGoal[] GetFinancialGoalsByUser(int userId)
        {
            return goals.Where(goal => goal != null && goal.UserId == userId).ToArray();
        }
        private void IncreaseArraySize()
        {
            FinancialGoal[] newGoals = new FinancialGoal[goals.Length * 2];
            Array.Copy(goals, newGoals, goals.Length);
            goals = newGoals;
        }
    }

}
