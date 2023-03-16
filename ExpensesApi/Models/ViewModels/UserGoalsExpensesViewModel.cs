namespace ExpensesApi.Models.ViewModels
{
    public class UserGoalsExpensesViewModel : MainExpensesViewModel
    {
        public IDictionary<string, decimal> UserGoals { get; set; }

        public IDictionary<string, decimal> UserExpenses { get; set; }

        public IDictionary<string, decimal> Result { get; set; }
    }
}
