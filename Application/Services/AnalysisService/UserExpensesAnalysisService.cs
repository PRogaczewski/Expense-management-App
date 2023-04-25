using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Exceptions;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses;
using Application.IServices.ExpensesList;
using Domain.Categories;

namespace Application.Services.AnalysisService
{
    public class UserExpensesAnalysisService : IUserExpensesAnalysisService
    {
        private readonly IExpensesListService _expensesListService;

        private readonly IExpensesService _expensesService;

        public UserExpensesAnalysisService(IExpensesListService expensesListService, IExpensesService expensesService)
        {
            _expensesListService = expensesListService;
            _expensesService = expensesService;
        }

        #region ExpensesAnalysis
        public async Task<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month, UserExpensesListDtoModel model = null)
        {
            var currentDay = DateTime.Now.Day;

            int monthEnding;
            int weekEnding;

            int monthBegining;
            int weekBegining;

            if ((int)DateTime.Now.DayOfWeek == 0)
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek - 6).Day;
            else
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek + 1).Day;

            if (weekBegining > currentDay)
            {
                monthBegining = DateTime.Now.Month - 1;
                monthEnding = DateTime.Now.Month;
            }
            else
            {
                monthBegining = DateTime.Now.Month;
                monthEnding = DateTime.Now.Month;
            }

            weekEnding = new DateTime(int.Parse(year), monthBegining, weekBegining).AddDays(6).Day;

            var beginDate = new DateTime(int.Parse(year), monthBegining, weekBegining);
            var endDate = new DateTime(int.Parse(year), monthEnding, weekEnding);

            if(model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var models = model.Expenses
               .Where(e => e.CreatedDate >= beginDate && e.CreatedDate <= endDate)
               .ToList();

            return ExpensesByCategory(id, models);
        }

        public async Task<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var models = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                .ToList();

            return ExpensesByCategory(id, models);
        }

        public async Task<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var models = model.Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();

            return ExpensesByCategory(id, models);
        }

        public async Task<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var firstMonthModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear && e.CreatedDate.Month.ToString() == firstMonth)
                .ToList();

            var secondMonthModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear && e.CreatedDate.Month.ToString() == secondMonth)
                .ToList();

            var firstMonthResult = ExpensesByCategory(id, firstMonthModels);
            var secondMonthResult = ExpensesByCategory(id, secondMonthModels);

            return CompareByCategory(firstMonthResult, secondMonthResult);
        }

        public async Task<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var firstYearModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear)
                .ToList();

            var secondYearModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear)
                .ToList();

            var firstYearResult = ExpensesByCategory(id, firstYearModels);
            var secondYearResult = ExpensesByCategory(id, secondYearModels);

            return CompareByCategory(firstYearResult, secondYearResult);
        }

        public async Task<IDictionary<string, decimal>[]> MonthlyGoals(int id, string year, string month, UserExpensesListDtoModel model = null)
        {
            var currentDate = month + year;

            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            var currentGoals = model.UserGoals
                .Where(g => g.MonthChosenForGoal.Month.ToString() + g.MonthChosenForGoal.Year.ToString() == currentDate)
                .Where(g => g.UserExpensesListId == id)
                .ToList();

            IDictionary<string, decimal> currentMonthGoal = new Dictionary<string, decimal>();
            //poprawic zwracanie nazw kategorii

            foreach (var goalsList in currentGoals)
            {
                foreach (var item in goalsList.UserCategoryGoals)
                {
                    if (currentMonthGoal.ContainsKey(item.Category.ToString()))
                    {
                        currentMonthGoal[item.Category.ToString()] += item.Limit;
                    }
                    else
                    {
                        currentMonthGoal.Add(item.Category.ToString(), item.Limit);
                    }
                }
            }

            var currentMonthExpenses = await ExpensesByCategoryMonth(id, year, month);
            var categories = currentMonthGoal.Keys.Intersect(currentMonthExpenses.Keys);

            IDictionary<string, decimal> currentMonthResult = new Dictionary<string, decimal>();
            IDictionary<string, decimal> currentMonthGoalExpenses = new Dictionary<string, decimal>();
            IDictionary<string, decimal> currentMonthResultPercentage = new Dictionary<string, decimal>();

            foreach (var category in categories)
            {
                if (currentMonthGoal.TryGetValue(category, out var firstDecimal) && currentMonthExpenses.TryGetValue(category, out var secondDecimal))
                {
                    currentMonthGoalExpenses.Add(category, secondDecimal);
                    currentMonthResult.Add(category, decimal.Round(firstDecimal - secondDecimal, 2));
                    currentMonthResultPercentage.Add(category, decimal.Round(1 - (secondDecimal / firstDecimal) * 100, 2));
                }
            }

            return new IDictionary<string, decimal>[] { currentMonthGoal, currentMonthGoalExpenses, currentMonthResult };
        }

        public async Task<decimal> TotalIncomesMonth(int id, string year, string month)
        {
            var income = await _expensesService.GetMonthlyIncome(id, year, month);

            return income.Income;
        }

        public async Task<decimal> TotalExpensesMonth(int id, string year, string month, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            return GetTotalPrice(id, year, model, month);
        }

        public async Task<decimal> TotalExpensesYear(int id, string year, UserExpensesListDtoModel model = null)
        {
            if (model == null)
            {
                model = await _expensesListService.GetExpensesList(id);
            }

            return GetTotalPrice(id, year, model);
        }

        private decimal GetTotalPrice(int id, string year, UserExpensesListDtoModel model, string? month = null)
        {
            List<UserExpensesDto> results;

            if (string.IsNullOrEmpty(month))
            {
                results = model.Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();
            }
            else
            {
                results = model.Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                    .ToList();
            }

            if (!results.Any())
                throw new BusinessException("Error occured during getting expenses.", 404);

            decimal totalSum = 0.0m;

            foreach (var expense in results)
            {
                totalSum += expense.Price;
            }

            return decimal.Round(totalSum, 2);
        }

        private IDictionary<string, decimal> ExpensesByCategory(int id, IList<UserExpensesDto> models)
        {
            var groupedModels = models.GroupBy(m => m.Category);

            Dictionary<ExpenseCategories, decimal> totalByCategories = new Dictionary<ExpenseCategories, decimal>();

            foreach (var group in groupedModels)
            {
                var total = 0.0m;

                foreach (var item in group)
                {
                    total += item.Price;
                }

                totalByCategories.Add(group.Key, total);
            }

            return totalByCategories.OrderByDescending(t => t.Value)
                .ToDictionary(t => t.Key.GetEnumDisplayName().ToString(), t => t.Value);
        }

        private IDictionary<string, decimal> CompareByCategory(IDictionary<string, decimal> firstMonthResult, IDictionary<string, decimal> secondMonthResult)
        {
            //Get the same categories
            var categories = firstMonthResult.Keys.Intersect(secondMonthResult.Keys);

            IDictionary<string, decimal> monthResult = new Dictionary<string, decimal>();

            foreach (var category in categories)
            {
                if (firstMonthResult.TryGetValue(category, out var firstDecimal) && secondMonthResult.TryGetValue(category, out var secondDecimal))
                {
                    monthResult.Add(category.ToString(), decimal.Round((1 - (firstDecimal / secondDecimal)) * 100 * -1, 1));
                }
            }

            return monthResult;
        }
        #endregion
    }
}
