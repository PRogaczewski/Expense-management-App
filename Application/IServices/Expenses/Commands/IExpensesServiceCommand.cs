using Application.Dto.Models.Expenses;

namespace Application.IServices.Expenses.Commands
{
    public interface IExpensesServiceCommand
    {
        Task CreateExpense(UserExpensesModel model);

        Task UpdateExpense(UserExpensesModel model, int id);

        Task DeleteExpense(int id);

        Task CreateExpensesGoal(UserExpenseGoalDto model);

        Task AddMonthlyIncome(UserIncomeModel income);
    }
}
