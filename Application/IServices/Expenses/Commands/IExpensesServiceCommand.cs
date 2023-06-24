using Application.Dto.Models.Expenses;

namespace Application.IServices.Expenses.Commands
{
    public interface IExpensesServiceCommand
    {
        Task CreateExpense(UserExpensesModel model);

        Task CreateExpensesGoal(UserExpenseGoalDto model);

        Task AddMonthlyIncome(UserIncomeModel income);
    }
}
