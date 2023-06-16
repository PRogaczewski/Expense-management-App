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

        public async Task<UserExpensesListResponse> GetUserInitialData(int id)
        {
            var model = await _expensesListService.GetExpensesList(id);

            var incomes = await _analysisService.TotalIncomesMonth(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(),model);
            var totalIncomes = await _analysisService.TotalIncomesMonth(DateTime.Now.Year.ToString(), string.Empty, model);
            //var incomes = await _analysisService.TotalIncomesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            var outgoings = await _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var totalByCategories = (await _analysisService.ExpensesByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model)).ToDictionary(k => k.Key, v => v.Value);
            var currentWeekByCategories = await _analysisService.ExpensesByCategoryCurrentWeek(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var userGoals = await _analysisService.MonthlyGoals(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var annualSummaryByMonths = await _analysisService.AnnualExpensesByMonth(DateTime.Now.Year.ToString(), model);
            var totalOutgoingsYear = await _analysisService.TotalExpensesYear(id, DateTime.Now.Year.ToString(), model);

            var compareToLastMonth = await _analysisService.CompareByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), (DateTime.Now.Month - 1).ToString(), model);
            var totalInLastMonth = await _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), (DateTime.Now.Month - 1).ToString(), model);

            return UserExpensesListResponse.CreateService(incomes, totalIncomes, outgoings, annualSummaryByMonths, totalOutgoingsYear, totalByCategories, currentWeekByCategories, compareToLastMonth, totalInLastMonth, userGoals);
        }
    }
}
