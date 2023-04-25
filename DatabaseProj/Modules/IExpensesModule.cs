using Domain.Entities.Models;

namespace Domain.Modules
{
    public interface IExpensesModule
    {
        Task CreateExpense(UserExpense model);

        Task<bool> CreateExpensesGoal(UserExpenseGoal model);

        Task<UserIncome> GetMonthlyIncome(int id, string year, string month);

        Task AddMonthlyIncome(UserIncome income);
    }
}
