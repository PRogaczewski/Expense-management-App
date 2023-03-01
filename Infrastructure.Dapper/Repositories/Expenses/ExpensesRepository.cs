using Dapper;
using Domain.Entities.Models;
using Domain.Modules;
using Infrastructure.Dapper.Database;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Dapper.Repositories.Expenses
{
    public class ExpensesRepository : DataConnection, IExpensesModule
    {
        public ExpensesRepository(IConfiguration configuration) : base(configuration)
        { }

        //public UserExpense GetExpensesList(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public void CreateExpense(UserExpense model)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("INSERT INTO Expenses (Category, Price, UserExpensesListId, CreatedDate) VALUES (@category, @price, @userExpensesListId, @createdDate)",
                        new { category = model.Category, price = model.Price, userExpensesListId = model.UserExpensesListId, createdDate = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateExpensesGoal(UserExpenseGoal model)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var listId = conn.QuerySingle<int>("INSERT INTO UserExpensesGoals (UserExpensesListId, MonthChosenForGoal, CreatedDate) OUTPUT INSERTED.Id VALUES (@userExpensesListId, @monthChosenForGoal, @createDate)",
                       new { userExpensesListId = model.UserExpensesListId, monthChosenForGoal = model.MonthChosenForGoal, createDate = DateTime.Now });

                    conn.Execute("INSERT INTO UserGoals (UserExpenseGoalId, Category, Limit) VALUES (@userExpenseGoalId, @category, @limit)",
                        model.UserCategoryGoals.Select(g => new { userExpenseGoalId = listId, category = g.Category, limit = g.Limit }));
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserIncome GetMonthlyIncome(int id, string year, string month)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var income = conn.Query<UserIncome>("SELECT * FROM UserIncomes WHERE UserExpensesListId = @id AND MONTH(CreatedDate) = @month AND YEAR(CreatedDate) = @year",
                                                new { id = id, month = month, year = year });

                    return income.FirstOrDefault();
                }

                var test = new SqlConnection();
                var s = test.State;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddMonthlyIncome(UserIncome income)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var currentIncome = conn.Query<UserIncome>("SELECT * FROM UserIncomes WHERE UserExpensesListId = @id AND WHERE MONTH(CreatedDate) = @month", new { id = income.UserExpensesListId, month = DateTime.Now.Month }).FirstOrDefault();

                    if (currentIncome != null)
                    {
                        var newIncome = income.Income + currentIncome.Income;

                        conn.Execute("UPDATE UserIncomes SET UpdateDate = @updateDate, Income = @income WHERE UserExpensesListId = @id",
                            new { updateDate = DateTime.Now, id = income.UserExpensesListId, income = newIncome });
                    }
                    else
                    {
                        conn.Execute("INSERT INTO UserIncomes (UserExpensesListId, Income, CreatedDate, UpdateDate) VALUES (@userExpensesListId, @income, @createdDate, UpdateDate)",
                            new { userExpensesListId = income.UserExpensesListId, income = income.Income, createdDate = DateTime.Now });
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
