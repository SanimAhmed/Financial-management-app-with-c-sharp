using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Csharp_1
{
    using System;

    public class BorrowingEvent
    {
        public string LenderName { get; private set; }
        public decimal AmountBorrowed { get; private set; }
        public DateTime BorrowDate { get; private set; }
        public DateTime? PaymentDate { get; private set; } // Nullable to handle unpaid debts

        public BorrowingEvent(string lenderName, decimal amountBorrowed, DateTime borrowDate)
        {
            LenderName = lenderName;
            AmountBorrowed = amountBorrowed;
            BorrowDate = borrowDate;
            PaymentDate = null;
        }

        public void RecordPayment(DateTime paymentDate)
        {
            PaymentDate = paymentDate;
        }
    }

    public class DebtTracker
    {
        private BorrowingEvent[] Borrowings;
        private int borrowingsCount;

        public DebtTracker(int maxBorrowingEvents)
        {
            Borrowings = new BorrowingEvent[maxBorrowingEvents];
            borrowingsCount = 0;
        }

        public void AddBorrowing(string lenderName, decimal amount, DateTime borrowDate)
        {
            if (borrowingsCount >= Borrowings.Length)
            {
                throw new InvalidOperationException("Maximum borrowing events reached");
            }

            Borrowings[borrowingsCount] = new BorrowingEvent(lenderName, amount, borrowDate);
            borrowingsCount++;
        }

        public void RecordPayment(int borrowingIndex, DateTime paymentDate)
        {
            if (borrowingIndex < 0 || borrowingIndex >= borrowingsCount)
            {
                throw new ArgumentOutOfRangeException("Invalid borrowing index");
            }

            Borrowings[borrowingIndex].RecordPayment(paymentDate);
        }

        // Additional methods as needed, e.g., to list all borrowings, list unpaid debts, etc.
    }

}
