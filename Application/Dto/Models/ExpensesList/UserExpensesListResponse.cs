namespace Application.Dto.Models.ExpensesList
{
    public class UserExpensesListResponse
    {
        public decimal Incomes { get; set; }

        public decimal Outgoings { get; set; }

        public decimal MonthlyResult { get; set; }

        public IDictionary<string, decimal> TotalMonthByCategories { get; set; }

        public IDictionary<string, decimal> CurrentWeekByCategories { get; set; }

        public IDictionary<string, decimal> CompareLastMonthByCategories { get; set; }

        public IDictionary<string, decimal> UserGoals { get; set; }

        public IDictionary<string, decimal> UserExpenses { get; set; }

        public IDictionary<string, decimal> Result { get; set; }
    }
}
