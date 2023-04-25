using Application.Dto.Models.Expenses;

namespace Application.IServices.Expenses
{
    public interface IExpensesService
    {
        Task CreateExpense(UserExpensesModel model);

        Task<bool> CreateExpensesGoal(UserExpenseGoalDto model);

        Task<UserIncomeDto> GetMonthlyIncome(int id, string year, string month);

        Task AddMonthlyIncome(UserIncomeModel income);
    }
}
