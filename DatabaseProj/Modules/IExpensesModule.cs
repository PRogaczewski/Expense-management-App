using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Domain.Modules
{
    public interface IExpensesModule
    {
        Task CreateExpense(UserExpense model);

        Task<bool> CreateExpensesGoal(UserExpenseGoal model);

        Task DeleteExpensesGoal(DateTimeWithIdRequestModel id);

        Task<bool> UpdateExpensesGoal(UserExpenseGoal model);

        Task<UserIncome> GetMonthlyIncome(int id, string year, string month);

        Task AddMonthlyIncome(UserIncome income);
    }
}
