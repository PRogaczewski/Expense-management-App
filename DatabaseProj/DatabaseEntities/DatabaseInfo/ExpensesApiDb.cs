using DatabaseProj.Database.Models;
using DatabaseProj.DatabaseEntities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProj.DatabaseEntities.ConnectionInfo
{
    public class ExpensesApiDb : DbContext
    {
        public ExpensesApiDb(DbContextOptions<ExpensesApiDb> options) : base(options) {}

        /// <summary>
        /// Db entity for user expense
        /// </summary>
        public DbSet<UserExpense> Expenses { get; set; }

        /// <summary>
        /// Db entity list for user expenses
        /// </summary>
        public DbSet<UserExpensesList> ExpensesLists { get; set; }

        /// <summary>
        /// Db entity for user expenses goals
        /// </summary>
        public DbSet<UserGoal> UserGoals { get; set; }

        /// <summary>
        /// Db entity list for user monthly expenses goals
        /// </summary>
        public DbSet<UserExpenseGoal> UserExpensesGoals { get; set; }

        /// <summary>
        /// Db entity for user incomes
        /// </summary>
        public DbSet<UserIncome> UserIncomes { get; set; }
    }
}
