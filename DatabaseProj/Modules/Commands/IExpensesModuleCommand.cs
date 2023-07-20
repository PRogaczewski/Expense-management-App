using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Domain.Modules.Commands
{
    public interface IExpensesModuleCommand
    {
        Task CreateExpense(UserExpense model);

        Task UpdateExpense(UserExpense model, int id);

        Task DeleteExpense(int id);

        Task CreateExpensesGoal(UserExpenseGoal model);

        Task DeleteExpensesGoal(DateTimeWithIdRequestModel id);

        Task UpdateExpensesGoal(UserExpenseGoal model);

        Task AddMonthlyIncome(UserIncome income);
    }
}
