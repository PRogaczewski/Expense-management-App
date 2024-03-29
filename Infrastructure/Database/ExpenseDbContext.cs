﻿using Application.Exceptions;
using Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.EF.Database
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options) 
        {
            try
            {
                var creator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (creator != null)
                {
                    if(!creator.CanConnect())
                    {
                        creator.Create();
                    }
                    if(!creator.HasTables())
                    {
                        creator.CreateTables();
                    }
                }
            }
            catch (DatabaseException ex)
            {

            }
            
        }

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

        /// <summary>
        /// User db
        /// </summary>
        public DbSet<UserApplication> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserApplication>(e =>
            {
                e.HasMany(w => w.ExpensesLists)
                .WithOne(c => c.UserApplication)
                .HasForeignKey(c => c.UserApplicationId);
            });
        }
    }
}
