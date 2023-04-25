using Application.Dto.Models.ExpensesList;

namespace Application.IServices.AnalysisService
{
    public interface IUserExpensesAnalysisService
    {
        Task<decimal> TotalIncomesMonth(int id, string year, string month);

        Task<decimal> TotalExpensesMonth(int id, string year, string month, UserExpensesListDtoModel model = null);

        Task<decimal> TotalExpensesYear(int id, string year, UserExpensesListDtoModel model = null);

        Task<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month, UserExpensesListDtoModel model = null);

        Task<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month, UserExpensesListDtoModel model = null);

        Task<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year, UserExpensesListDtoModel models = null);

        Task<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth, UserExpensesListDtoModel model = null);

        Task<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear, UserExpensesListDtoModel model = null);

        Task<IDictionary<string, decimal>[]> MonthlyGoals(int id, string year, string month, UserExpensesListDtoModel model = null);
    }
}
