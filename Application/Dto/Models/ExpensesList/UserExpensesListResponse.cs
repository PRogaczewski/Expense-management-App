namespace Application.Dto.Models.ExpensesList
{
    public class UserExpensesListResponse
    {
        protected UserExpensesListResponse()
        {
            AnnualSummary = new Dictionary<int, decimal>();
            TotalMonthByCategories = new Dictionary<string, decimal>();
            CurrentWeekByCategories = new Dictionary<string, decimal>();
            CompareLastMonthByCategories = new Dictionary<string, decimal>();
            UserGoals = new Dictionary<string, decimal>();
            UserExpenses = new Dictionary<string, decimal>();
            Result = new Dictionary<string, decimal>();
        }

        public static UserExpensesListResponse CreateService(decimal incomes, decimal totalYearIncomes, decimal outgoings, 
            IDictionary<int, decimal> annualSummaryByMonths, decimal totalYearOutgoings,IDictionary<string, decimal> totalByCategories,
            IDictionary<string, decimal> currentWeekByCategories, IDictionary<string, decimal> compareToLastMonth,
            decimal totalInMonth, IDictionary<string, decimal>[] userGoals)
        {
            return new UserExpensesListResponse()
            {
                Incomes = incomes,
                TotalYearIncomes = totalYearIncomes,
                Outgoings = outgoings,
                MonthlyResult = incomes - outgoings,
                AnnualSummary = annualSummaryByMonths,
                TotalYearOutgoings = totalYearOutgoings,
                TotalMonthByCategories = totalByCategories,
                CurrentWeekByCategories = currentWeekByCategories,
                CompareLastMonthByCategories = compareToLastMonth,
                PreviousMonthTotalResult = totalInMonth,
                UserGoals = userGoals[0],
                UserExpenses = userGoals[1],
                Result = userGoals[2],
            };
        }

        public decimal Incomes { get; set; }

        public decimal TotalYearIncomes { get; set; }

        public decimal Outgoings { get; set; }

        public decimal MonthlyResult { get; set; }

        public decimal PreviousMonthTotalResult { get; set; }

        public decimal TotalYearOutgoings { get; set; }

        public IDictionary<int, decimal> AnnualSummary { get; set; }

        public IDictionary<string, decimal> TotalMonthByCategories { get; set; }

        public IDictionary<string, decimal> CurrentWeekByCategories { get; set; }

        public IDictionary<string, decimal> CompareLastMonthByCategories { get; set; }

        public IDictionary<string, decimal> UserGoals { get; set; }

        public IDictionary<string, decimal> UserExpenses { get; set; }

        public IDictionary<string, decimal> Result { get; set; }
    }
}
