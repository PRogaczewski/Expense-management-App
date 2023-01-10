using Application.Dto.Models.Expenses;
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
        public IDictionary<string, decimal> ExpensesByCategoryCurrentWeek(int id, string year, string month)
        {
            var currentDay = DateTime.Now.Day;
            int weekBegining;

            if (DateTime.Now.DayOfWeek == 0)
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek - 6).Day;
            else
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek + 1).Day;


            var models = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month && e.CreatedDate.Day >= weekBegining && e.CreatedDate.Day <= currentDay)
                .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<string, decimal> ExpensesByCategoryMonth(int id, string year, string month)
        {
            var models = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<string, decimal> ExpensesByCategoryYear(int id, string year)
        {
            var models = _expensesListService.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<string, decimal> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth)
        {
            var firstMonthModels = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear && e.CreatedDate.Month.ToString() == firstMonth)
                .ToList();

            var secondMonthModels = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear && e.CreatedDate.Month.ToString() == secondMonth)
                .ToList();

            var firstMonthResult = ExpensesByCategory(id, firstMonthModels);
            var secondMonthResult = ExpensesByCategory(id, secondMonthModels);

            return CompareByCategory(firstMonthResult, secondMonthResult);
        }

        public IDictionary<string, decimal> CompareByCategoryYear(int id, string firstYear, string secondYear)
        {
            var firstYearModels = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear)
                .ToList();

            var secondYearModels = _expensesListService.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear)
                .ToList();

            var firstYearResult = ExpensesByCategory(id, firstYearModels);
            var secondYearResult = ExpensesByCategory(id, secondYearModels);

            return CompareByCategory(firstYearResult, secondYearResult);
        }

        public IDictionary<string, decimal>[] MonthlyGoals(int id, string year, string month)
        {
            var currentDate = month + year;

            var currentGoals = _expensesListService.
                GetExpensesList(id).UserGoals.
                Where(g => g.MonthChosenForGoal.Month.ToString() + g.MonthChosenForGoal.Year.ToString() == currentDate)
                .Where(g => g.UserExpensesListId == id)
                .ToList();

            IDictionary<string, decimal> currentMonthGoal = new Dictionary<string, decimal>();

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

            var currentMonthExpenses = ExpensesByCategoryMonth(id, year, month);
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

        public decimal TotalIncomesMonth(int id, string year, string month)
        {
            var income = _expensesService.GetMonthlyIncome(id, year, month);

            return income.Income;
        }

        public decimal TotalExpensesMonth(int id, string year, string month)
        {
            return GetTotalPrice(id, year, month);
        }

        public decimal TotalExpensesYear(int id, string year)
        {
            return GetTotalPrice(id, year);
        }

        private decimal GetTotalPrice(int id, string year, string? month = null)
        {
            List<UserExpensesDto> models = null;

            if (string.IsNullOrEmpty(month))
            {
                models = _expensesListService.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();
            }
            else
            {
                models = _expensesListService.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                    .ToList();
            }

            if (models is null)
                throw new BusinessException("Error occured during getting expenses.", 404);

            decimal totalSum = 0.0m;

            foreach (var expense in models)
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
                .ToDictionary(t => t.Key.ToString(), t => t.Value);
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
