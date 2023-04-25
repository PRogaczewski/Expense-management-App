using Application.Dto.Models.Expenses;

namespace ExpensesApi.Models.ViewModels
{
    public class UserExpensesListModelViewModel : ViewModelAbstract
    {
        public int id { get; set; }

        public int UserApplicationId { get; set; }

        public string Name { get; set; }

        public List<UserExpensesDto> Expenses { get; set; } 

        public List<UserExpenseGoalDto> UserGoals { get; set; } 

        public List<UserIncomeDto> UserIncomes { get; set; }

        public UserExpensesListModelViewModel()
        {
            Expenses = new List<UserExpensesDto>();
            UserGoals = new List<UserExpenseGoalDto>();
            UserIncomes = new List<UserIncomeDto>();
        }
    }
}
