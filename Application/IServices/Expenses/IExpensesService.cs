using Application.Dto.Models.Expenses;

namespace Application.IServices.Expenses
{
    public interface IExpensesService
    {
        //public UserExpensesDto GetExpensesList(int id);

        public void CreateExpense(UserExpensesModel model);

        public bool CreateExpensesGoal(UserExpenseGoalDto model);

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month);

        public void AddMonthlyIncome(UserIncomeModel income);
    }
}
