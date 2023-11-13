using System.Data.Entity;

public class FinancialManagerContext : DbContext
{
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<FinancialGoal> FinancialGoals { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<BillReminder> BillReminders { get; set; }
    public DbSet<BorrowingEvent> BorrowingEvents { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Liability> Liabilities { get; set; }

    public FinancialManagerContext() : base("name=FinancialManagerContext")
    {
        // Configuration settings (if needed)
    }

    // Override OnModelCreating if you need to configure model properties further
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Example: modelBuilder.Entity<UserAccount>().ToTable("Users");
        // Add further configurations here
    }
}
