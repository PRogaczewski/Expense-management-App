using Application.Exceptions;
using Dapper;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules;
using Infrastructure.Dapper.Database;
using Infrastructure.SeedData.Service;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Dapper.Repositories.ExpensesList
{
    public class ExpensesListRepository : DataConnection//, ICategoryService, IExpensesListModule
    {
        private readonly IExpensesSeeder _seeder;

        public ExpensesListRepository(IConfiguration configuration, IExpensesSeeder seeder) : base(configuration)
        {
            _seeder = seeder;
        }

        public IEnumerable<string> GetCategories()
        {
            return ((ICategoryService)this).GetExpenseCategories();
        }

        public IEnumerable<UserExpensesList> GetExpensesLists()
        {
            try
            {
                var expensesDict = new Dictionary<int, UserExpensesList>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    var expensesLists = connection.Query<UserExpensesList, UserExpense, UserExpensesList>("SELECT EL.Id, EL.Name, EL.CreatedDate, EL.UpdateDate, E.Id as Expenses_Id, E.Category"
                        + ", E.Price, E.UserExpensesListId, E.CreatedDate FROM ExpensesLists EL "
                        + "LEFT JOIN Expenses E ON EL.Id = E.UserExpensesListId", (expensesList, expense) =>
                        {
                            UserExpensesList list;

                            if (!expensesDict.TryGetValue(expensesList.Id, out list))
                            {
                                list = expensesList;
                                list.Expenses = new List<UserExpense>();
                                expensesDict.Add(expensesList.Id, list);
                            }
                            if (expense.UserExpensesListId > 0)
                                list.Expenses.Add(expense);

                            return list;

                        }, splitOn: "Expenses_Id");

                    var results = expensesLists.Distinct().ToList();

                    if (results is null)
                        throw new NotFoundException("Expenses lists not found.");

                    return results;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserExpensesList GetExpensesList(int id)
        {
            try
            {
                var goalsDict = new Dictionary<int, UserExpenseGoal>();
                var expensesDict = new Dictionary<int, UserExpensesList>();
                UserExpensesList list = default;

                using (var conn = new SqlConnection(_connectionString))
                {
                    var expensesLists = conn.Query<UserExpensesList, UserExpense, UserExpensesList>("SELECT EL.Id, EL.Name, EL.CreatedDate, EL.UpdateDate, E.Id as Expenses_Id, E.Category"
                        + ", E.Price, E.UserExpensesListId, E.CreatedDate FROM ExpensesLists EL "
                        + "LEFT JOIN Expenses E ON EL.Id = E.UserExpensesListId WHERE EL.Id = @id", (expensesList, expense) =>
                        {
                            if (list == default)
                            {
                                list = expensesList;
                            }
                            if (expense.UserExpensesListId > 0)
                                list.Expenses.Add(expense);

                            return list;

                        }, new { id = id }, splitOn: "Expenses_Id");

                    var result = expensesLists.First();

                    var incomes = conn.Query<UserIncome>("SELECT * FROM UserIncomes WHERE UserExpensesListId = @id", new { id = id });

                    var goals = conn.Query<UserExpenseGoal, UserGoal, UserExpenseGoal>("SELECT U.Id, U.UserExpensesListId, U.MonthChosenForGoal, U.CreatedDate, G.Id as UserCategoryGoals_Id"
                        + ", G.Category, G.Limit, G.UserExpenseGoalId "
                        + "FROM UserExpensesGoals U "
                        + "LEFT JOIN UserGoals G "
                        + "ON U.Id = G.UserExpenseGoalId WHERE UserExpensesListId = @id", (expenseGoal, userGoal) =>
                        {
                            UserExpenseGoal goal;

                            if (!goalsDict.TryGetValue(expenseGoal.Id, out goal))
                            {
                                goal = expenseGoal;
                                goal.UserCategoryGoals = new List<UserGoal>();
                                goalsDict.Add(expenseGoal.Id, goal);
                            }
                            if (userGoal.UserExpenseGoalId > 0)
                                goal.UserCategoryGoals.Add(userGoal);

                            return goal;
                        }, new { id = id }, splitOn: "UserCategoryGoals_Id");


                    var goalsResult = goals.Distinct().ToList();

                    result.UserIncomes = incomes.ToList();
                    result.UserGoals = goalsResult.ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateExpensesList(UserExpensesList model)
        {
            if (GetExpensesLists().Any(e => e.Name == model.Name))
                throw new BusinessException("List with the same name already exists.", 409);

            try
            {
                int listId;

                using (var connection = new SqlConnection(_connectionString))
                {
                    listId = connection.QuerySingle<int>("INSERT INTO ExpensesLists (Name, CreatedDate) OUTPUT INSERTED.Id VALUES (@name, @date)", new { name = model.Name, date = DateTime.Now });
                }

                if (model.Name.ToLower().Contains("seeder"))
                {
                    model = _seeder.Seed(model.Name);

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        connection.Execute("INSERT INTO Expenses (Category, Price, CreatedDate, UserExpensesListId) VALUES (@category, @price, @createdDate, @userExpensesListId)", model.Expenses.Select(m => new { category = m.Category, price = m.Price, createdDate = m.CreatedDate, userExpensesListId = listId }));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateExpensesList(UserExpensesList model, int id)
        {
            if (GetExpensesLists().Any(e => e.Name == model.Name))
                throw new BusinessException("List with this name exists.", 409);

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    //connection.Query<UserExpensesList>("UPDATE ExpensesLists SET Name = @name, UpdateDate = @date WHERE Id = @id", new { name = model.Name, date = DateTime.Now, id = id });
                    connection.Execute("UPDATE ExpensesLists SET Name = @name, UpdateDate = @date WHERE Id = @id", new { name = model.Name, date = DateTime.Now, id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteExpensesList(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    //connection.Query<UserExpensesList>("DELETE FROM ExpensesLists WHERE Id = @id", new { id = id });
                    connection.Execute("DELETE FROM ExpensesLists WHERE Id = @id", new { id = id });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
