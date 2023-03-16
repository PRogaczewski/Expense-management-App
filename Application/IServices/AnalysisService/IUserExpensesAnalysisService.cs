using Application.Dto.Models.ExpensesList;

namespace Application.IServices.AnalysisService
{
    public interface IUserExpensesAnalysisService
    {
        public decimal TotalIncomesMonth(int id, string year, string month);

        public decimal TotalExpensesMonth(int id, string year, string month, UserExpensesListDtoModel model = null);

        public decimal TotalExpensesYear(int id, string year, UserExpensesListDtoModel model = null);

        public IDictionary<string, decimal> ExpensesByCategoryCurrentWeek(int id, string year, string month, UserExpensesListDtoModel model = null);

        public IDictionary<string, decimal> ExpensesByCategoryMonth(int id, string year, string month, UserExpensesListDtoModel model = null);

        public IDictionary<string, decimal> ExpensesByCategoryYear(int id, string year, UserExpensesListDtoModel models = null);

        public IDictionary<string, decimal> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth, UserExpensesListDtoModel model = null);

        public IDictionary<string, decimal> CompareByCategoryYear(int id, string firstYear, string secondYear, UserExpensesListDtoModel model = null);

        public IDictionary<string, decimal>[] MonthlyGoals(int id, string year, string month, UserExpensesListDtoModel model = null);
    }
}
