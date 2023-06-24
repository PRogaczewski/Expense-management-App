using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Domain.Modules.Commands
{
    public interface IExpensesModuleCommand
    {
        Task CreateExpense(UserExpense model);

        Task CreateExpensesGoal(UserExpenseGoal model);

        Task DeleteExpensesGoal(DateTimeWithIdRequestModel id);

        Task<bool> UpdateExpensesGoal(UserExpenseGoal model);

        Task AddMonthlyIncome(UserIncome income);
    }
}
