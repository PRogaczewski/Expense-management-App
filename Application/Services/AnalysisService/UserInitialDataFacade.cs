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

            var incomes = await _analysisService.TotalIncomesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            var outgoings = await _analysisService.TotalExpensesMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var totalByCategories = (await _analysisService.ExpensesByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model)).ToDictionary(k => k.Key, v => v.Value);
            var currentWeekByCategories = await _analysisService.ExpensesByCategoryCurrentWeek(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);
            var userGoals = await _analysisService.MonthlyGoals(id, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), model);

            var compareToLastMonth = await _analysisService.CompareByCategoryMonth(id, DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), (DateTime.Now.Month - 1).ToString(), model);

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
