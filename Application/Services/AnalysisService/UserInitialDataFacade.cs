using Application.Dto.Models.ExpensesList;
using Application.IServices.AnalysisService;
using Application.IServices.ExpensesList;

namespace Application.Services.AnalysisService
{
    public class UserInitialDataFacade : IUserInitialData
    {
        private readonly IUserExpensesAnalysisService _analysisService;

        private readonly IExpensesListService _expensesListService;

        public UserInitialDataFacade(IUserExpensesAnalysisService userExpensesAnalysisService, IExpensesListService expensesListService)
        {
            _analysisService = userExpensesAnalysisService;
            _expensesListService = expensesListService;
        }

        public UserExpensesListResponse GetUserInitialData(int id)
        {
            var model = _expensesListService.GetExpensesList(id);

            var incomes = _analysisService.TotalIncomesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            var outgoings = _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var totalByCategories = _analysisService.ExpensesByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model).ToDictionary(k => k.Key, v => v.Value);
            var currentWeekByCategories = _analysisService.ExpensesByCategoryCurrentWeek(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var userGoals = _analysisService.MonthlyGoals(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            IDictionary<string, decimal> compareToLastMonth = new Dictionary<string, decimal>();

            if (DateTime.Now.Day >= 26)
            {
                compareToLastMonth = _analysisService.CompareByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), (DateTime.Now.Month - 1).ToString(), model);
            }

            return new UserExpensesListResponse
            {
                Incomes = incomes,
                Outgoings = outgoings,
                MonthlyResult = incomes - outgoings,
                TotalMonthByCategories = totalByCategories,
                CurrentWeekByCategories = currentWeekByCategories,
                CompareLastMonthByCategories = compareToLastMonth,
                UserGoals = userGoals[0],
                UserExpenses = userGoals[1],
                Result = userGoals[2],
            };
        }
    }
}
