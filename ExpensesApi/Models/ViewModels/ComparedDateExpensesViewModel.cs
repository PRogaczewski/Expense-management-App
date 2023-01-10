
namespace ExpensesApi.Models.ViewModels
{
    public class ComparedDateExpensesViewModel : MainExpensesViewModel
    {
        //public IDictionary<ExpenseCategories, decimal> CompareLastMonthByCategories { get; set; }
        public IDictionary<string, decimal> CompareLastMonthByCategories { get; set; }
    }
}
