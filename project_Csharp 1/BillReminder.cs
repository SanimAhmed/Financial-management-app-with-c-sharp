using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    namespace project_Csharp_1
    {
        public class BillReminder
        {
            public int BillReminderId { get; set; }
            public int UserId { get; set; }
            public string BillName { get; set; }
            public decimal Amount { get; set; }
            public DateTime DueDate { get; set; }
            public bool IsPaid { get; set; }

            public BillReminder(int userId, string billName, decimal amount, DateTime dueDate)
            {
                UserId = userId;
                BillName = billName;
                Amount = amount;
                DueDate = dueDate;
                IsPaid = false; // Default to unpaid
            }
        }
    }

