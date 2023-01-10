namespace ExpensesApi.Models.ViewModels
{
    public class MainExpensesViewModel
    {
        public decimal Incomes { get; set; }

        public decimal Outgoings { get; set; }

        public decimal MonthlyResult { get; set; }

        //public IDictionary<ExpenseCategories, decimal> TotalMonthByCategories { get; set; }
        public IDictionary<string, decimal> TotalMonthByCategories { get; set; }

        //public IDictionary<ExpenseCategories, decimal> CurrentWeekByCategories { get; set; }
        public IDictionary<string, decimal> CurrentWeekByCategories { get; set; }
    }
}
