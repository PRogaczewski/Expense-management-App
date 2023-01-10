using Domain.Categories;

namespace Application.IServices.AnalysisService
{
    public interface IUserExpensesAnalysisService
    {
        public decimal TotalIncomesMonth(int id, string year, string month);

        public decimal TotalExpensesMonth(int id, string year, string month);

        public decimal TotalExpensesYear(int id, string year);

        //public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryCurrentWeek(int id, string year, string month);

        //public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryMonth(int id, string year, string month);

        //public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryYear(int id, string year);

        //public IDictionary<ExpenseCategories, decimal> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth);

        //public IDictionary<ExpenseCategories, decimal> CompareByCategoryYear(int id, string firstYear, string secondYear);

        //public IDictionary<ExpenseCategories, decimal>[] MonthlyGoals(int id, string year, string month);
        public IDictionary<string, decimal> ExpensesByCategoryCurrentWeek(int id, string year, string month);

        public IDictionary<string, decimal> ExpensesByCategoryMonth(int id, string year, string month);

        public IDictionary<string, decimal> ExpensesByCategoryYear(int id, string year);

        public IDictionary<string, decimal> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth);

        public IDictionary<string, decimal> CompareByCategoryYear(int id, string firstYear, string secondYear);

        public IDictionary<string, decimal>[] MonthlyGoals(int id, string year, string month);
    }
}
