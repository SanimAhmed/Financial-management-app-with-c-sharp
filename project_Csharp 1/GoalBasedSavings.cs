using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
   

    public class GoalBasedSavings
    {
        public string GoalName { get; private set; }
        public decimal TargetAmount { get; private set; }
        public DateTime TargetDate { get; private set; }
        public decimal MonthlyContribution { get; private set; }
        public decimal TotalSaved { get; private set; }
        public bool IsGoalReached => TotalSaved >= TargetAmount;

        public GoalBasedSavings(string goalName, decimal targetAmount, DateTime targetDate)
        {
            GoalName = goalName;
            TargetAmount = targetAmount;
            TargetDate = targetDate;
            CalculateMonthlyContribution();
            TotalSaved = 0;
        }

        public void CalculateMonthlyContribution()
        {
            var now = DateTime.Now;

            // Calculate the total number of months between the target date and now
            int months = ((TargetDate.Year - now.Year) * 12) + TargetDate.Month - now.Month;

            // Ensure at least one month for calculation
            months = Math.Max(months, 1);

            MonthlyContribution = TargetAmount / months;
        }


        public void UpdateGoal(string newGoalName, decimal newTargetAmount, DateTime newTargetDate)
        {
            GoalName = newGoalName;
            TargetAmount = newTargetAmount;
            TargetDate = newTargetDate;
            CalculateMonthlyContribution();
        }

        public void AddContribution(decimal amount)
        {
            TotalSaved += amount;
            if (IsGoalReached)
            {
                OnGoalReached();
            }
        }

        public void OnGoalReached()
        {
            Console.WriteLine($"Congratulations! You have reached your saving goal: {GoalName}.");
            // Additional logic for goal completion
        }

        public decimal GetRemainingAmount()
        {
            return TargetAmount - TotalSaved;
        }

        public void DisplayProgress()
        {
            decimal progressPercentage = (TotalSaved / TargetAmount) * 100;
            Console.WriteLine($"Goal: {GoalName}, Progress: {progressPercentage}%");
        }

        // Additional methods can be added as needed
    }

}
