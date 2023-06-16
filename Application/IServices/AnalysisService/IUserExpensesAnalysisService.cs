using Application.Dto.Models.ExpensesList;

namespace Application.IServices.AnalysisService
{
    public interface IUserExpensesAnalysisService
    {
        ValueTask<decimal> TotalIncomesMonth(int id, string year, string month);

        ValueTask<decimal> TotalIncomesMonth(string year, string month, UserExpensesListDtoModel model);

        ValueTask<decimal> TotalExpensesMonth(int id, string year, string month, UserExpensesListDtoModel model);

        ValueTask<decimal> TotalExpensesMonth(int id, string year, string month);

        ValueTask<decimal> TotalExpensesYear(int id, string year, UserExpensesListDtoModel model);

        ValueTask<decimal> TotalExpensesYear(int id, string year);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month, UserExpensesListDtoModel model);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month, UserExpensesListDtoModel model);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year, UserExpensesListDtoModel models);

        ValueTask<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year);

        ValueTask<IDictionary<int, decimal>> AnnualExpensesByMonth(string year, UserExpensesListDtoModel models);

        ValueTask<IDictionary<int, decimal>> AnnualExpensesByMonth(int id, string year);

        ValueTask<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth, UserExpensesListDtoModel model);

        ValueTask<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth);

        ValueTask<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear, UserExpensesListDtoModel model);

        ValueTask<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear);

        ValueTask<IDictionary<string, decimal>[]> MonthlyGoals(int id, string year, string month, UserExpensesListDtoModel model = null);
    }
}
