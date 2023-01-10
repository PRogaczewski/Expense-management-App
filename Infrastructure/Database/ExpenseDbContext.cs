using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Database
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) {}

        /// <summary>
        /// Db for user expense
        /// </summary>
        public DbSet<UserExpense> Expenses { get; set; }

        /// <summary>
        /// Db list for user expenses
        /// </summary>
        public DbSet<UserExpensesList> ExpensesLists { get; set; }

        /// <summary>
        /// Db for user expenses goals
        /// </summary>
        public DbSet<UserGoal> UserGoals { get; set; }

        /// <summary>
        /// Db list for user monthly expenses goals
        /// </summary>
        public DbSet<UserExpenseGoal> UserExpensesGoals { get; set; }

        /// <summary>
        /// Db for user incomes
        /// </summary>
        public DbSet<UserIncome> UserIncomes { get; set; }
    }
}
