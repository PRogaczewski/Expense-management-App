using Application.Dto.Models.Expenses;

namespace Application.Dto.Models.ExpensesList
{
    public class UserExpensesListDtoModel : UserExpensesListDto
    {
        public List<UserExpensesDto> Expenses { get; set; } = new List<UserExpensesDto>();

        public List<UserExpenseGoalDto> UserGoals { get; set; } = new List<UserExpenseGoalDto>();

        public List<UserIncomeDto> UserIncomes { get; set; } = new List<UserIncomeDto>();
    }
}
