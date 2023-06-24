using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Dto.Models.Helpers;
using Application.Exceptions;
using Application.IServices.AnalysisService;
using Application.IServices.Expenses.Queries;
using Application.IServices.ExpensesList.Queries;
using Domain.Categories;
using Domain.ValueObjects;

namespace Application.Services.AnalysisService
{
    public class UserExpensesAnalysisService : IUserExpensesAnalysisService
    {
        private readonly IExpensesListServiceQuery _expensesListService;

        private readonly IExpensesServiceQuery _expensesService;

        public UserExpensesAnalysisService(IExpensesListServiceQuery expensesListService, IExpensesServiceQuery expensesService)
        {
            _expensesListService = expensesListService;
            _expensesService = expensesService;
        }

        #region Public Methods
        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month, UserExpensesListDtoModel model)
        {
            var dates = GetWeekRange(year);

            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var models = model.Expenses
               .Where(e => e.CreatedDate >= dates.begin && e.CreatedDate <= dates.end);

            return await Task.Run(() =>
            {
                return ExpensesByCategory(id, models);
            });           
        }

        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryCurrentWeek(int id, string year, string month)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, month));

            var dates = GetWeekRange(year);

            model = model.Where(e => e.CreatedDate >= dates.begin && e.CreatedDate <= dates.end);

            return ExpensesByCategory(id, model);
        }

        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var models = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month);

            return await Task.Run(() =>
            {
                return ExpensesByCategory(id, models);
            });     
        }

        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryMonth(int id, string year, string month)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, month));

            return ExpensesByCategory(id, model);
        }

        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var models = model.Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year);

            return await Task.Run(() =>
            {
                return ExpensesByCategory(id, models);
            });       
        }

        public async ValueTask<IDictionary<string, decimal>> ExpensesByCategoryYear(int id, string year)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, string.Empty));

            return ExpensesByCategory(id, model);
        }

        public async ValueTask<IDictionary<int, decimal>> AnnualExpensesByMonth(string year, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var models = await Task.Run (() =>
            {
                return model.Expenses.Where(e => e.CreatedDate.Year.ToString() == year);
            });

            return ExpensesByMonth(models);
        }

        public async ValueTask<IDictionary<int, decimal>> AnnualExpensesByMonth(int id, string year)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, string.Empty));

            return ExpensesByMonth(model);
        }

        public async ValueTask<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var firstMonthModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear && e.CreatedDate.Month.ToString() == firstMonth);

            var secondMonthModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear && e.CreatedDate.Month.ToString() == secondMonth);

            var firstMonthResult = ExpensesByCategory(id, firstMonthModels);
            var secondMonthResult = ExpensesByCategory(id, secondMonthModels);

            return await Task.Run(() =>
            {
                return CompareByCategory(firstMonthResult, secondMonthResult);
            });          
        }

        public async ValueTask<IDictionary<string, decimal>> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth)
        {
            var model = await _expensesListService.GetExpensesByDate(new ExtendedDateTimeRequestModel(id, firstYear, firstMonth, secondYear, secondMonth));

            var firstMonthModels = model.FirstOrDefault(x => x.Case == Comparer.First)?.Expenses;
            var secondMonthModels = model.FirstOrDefault(x => x.Case == Comparer.Second)?.Expenses;

            if (firstMonthModels == null)
                firstMonthModels = new List<UserExpensesDto>();

            if (secondMonthModels == null)
                secondMonthModels = new List<UserExpensesDto>();

            var firstMonthResult = ExpensesByCategory(id, firstMonthModels);
            var secondMonthResult = ExpensesByCategory(id, secondMonthModels);

            return CompareByCategory(firstMonthResult, secondMonthResult);
        }

        public async ValueTask<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var firstYearModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear);

            var secondYearModels = model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear);

            var firstYearResult = ExpensesByCategory(id, firstYearModels);
            var secondYearResult = ExpensesByCategory(id, secondYearModels);

            return await Task.Run(() =>
            {
                return CompareByCategory(firstYearResult, secondYearResult);
            });         
        }

        public async ValueTask<IDictionary<string, decimal>> CompareByCategoryYear(int id, string firstYear, string secondYear)
        {
            var model = await _expensesListService.GetExpensesByDate(new ExtendedDateTimeRequestModel(id, firstYear, string.Empty, secondYear, string.Empty));

            var firstYearModels = model.FirstOrDefault(x => x.Case == Comparer.First)?.Expenses;
            var secondYearModels = model.FirstOrDefault(x => x.Case == Comparer.Second)?.Expenses;

            if (firstYearModels == null)
                firstYearModels = new List<UserExpensesDto>();

            if (secondYearModels == null)
                secondYearModels = new List<UserExpensesDto>();

            var firstYearResult = ExpensesByCategory(id, firstYearModels);
            var secondYearResult = ExpensesByCategory(id, secondYearModels);

            return CompareByCategory(firstYearResult, secondYearResult);
        }

        public async ValueTask<IDictionary<string, decimal>[]> MonthlyGoals(int id, string year, string month, UserExpensesListDtoModel model = null)
        {
            var currentDate = month + year;

            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var currentGoals = model.UserGoals
                .Where(g => g.MonthChosenForGoal.Month.ToString() + g.MonthChosenForGoal.Year.ToString() == currentDate)
                .Where(g => g.UserExpensesListId == id)
                .ToList();

            IDictionary<string, decimal> currentMonthGoal = new Dictionary<string, decimal>();

            foreach (var goalsList in currentGoals)
            {
                foreach (var item in goalsList.UserCategoryGoals)
                {
                    if (currentMonthGoal.ContainsKey(item.Category.ToString()))
                    {
                        currentMonthGoal[item.Category.GetEnumDisplayName().ToString()] += item.Limit;
                    }
                    else
                    {
                        currentMonthGoal.Add(item.Category.GetEnumDisplayName().ToString(), item.Limit);
                    }
                }
            }

            var currentMonthExpenses = await ExpensesByCategoryMonth(id, year, month, model);
            var categories = currentMonthGoal.Keys.Intersect(currentMonthExpenses.Keys).ToList();

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

        public async ValueTask<decimal> TotalIncomesMonth(int id, string year, string month)
        {
            var income = await _expensesService.GetMonthlyIncome(id, year, month);

            return income.Income;
        }


        public async ValueTask<decimal> TotalIncomesMonth(string year, string month, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var income = model.UserIncomes
                .Where(e => e.CreatedDate.Year.ToString() == year && (string.IsNullOrEmpty(month) || e.CreatedDate.Month.ToString() == month));

            var sum = await Task.Run(() =>
            {
                return income.Sum(x => x.Income);
            });

            return sum;
        }

        public async ValueTask<decimal> TotalExpensesMonth(int id, string year, string month, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var items = await Task.Run(() =>
            {
                return model.Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month);
            });         

            return GetTotalPrice(items);
        }

        public async ValueTask<decimal> TotalExpensesMonth(int id, string year, string month)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, month));

            return GetTotalPrice(model);
        }

        public async ValueTask<decimal> TotalExpensesYear(int id, string year, UserExpensesListDtoModel model)
        {
            if (model == null)
            {
                throw new NoModelProvidedException("Required model not found");
            }

            var items = model.Expenses
               .Where(e => e.CreatedDate.Year.ToString() == year);

            return await Task.Run(() =>
            {
                return GetTotalPrice(items);
            });  
        }

        public async ValueTask<decimal> TotalExpensesYear(int id, string year)
        {
            var model = await _expensesListService.GetExpensesByDate(new DateTimeWithIdRequestModel(id, year, string.Empty));

            return GetTotalPrice(model);
        }

        #endregion

        #region Private Methods
        private decimal GetTotalPrice(IEnumerable<UserExpensesDto> results)
        {
            decimal totalSum = 0.0m;

            if (!results.Any())
            {
                return decimal.Round(totalSum, 2);
            }

            foreach (var expense in results)
            {
                totalSum += expense.Price;
            }

            return decimal.Round(totalSum, 2);
        }

        private IDictionary<string, decimal> ExpensesByCategory(int id, IEnumerable<UserExpensesDto> models)
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

        private IDictionary<int, decimal> ExpensesByMonth(IEnumerable<UserExpensesDto> models)
        {
            var groupedModels = models.GroupBy(m => m.CreatedDate.Month);

            var totalByMonths = new Dictionary<int, decimal>();

            foreach (var group in groupedModels)
            {
                var total = 0.0m;

                foreach (var item in group)
                {
                    total += item.Price;
                }

                totalByMonths.Add(group.Key, total);
            }

            return totalByMonths.OrderByDescending(t => t.Value)
                .ToDictionary(t => t.Key, t => t.Value);
        }

        private IDictionary<string, decimal> CompareByCategory(IDictionary<string, decimal> firstMonthResult, IDictionary<string, decimal> secondMonthResult)
        {
            if (!firstMonthResult.Any())
            {
                return secondMonthResult.ToDictionary(x => x.Key, x => (x.Value / x.Value) * 100);
            }

            if (!secondMonthResult.Any())
            {
                return firstMonthResult.ToDictionary(x => x.Key, x => (x.Value / x.Value) * 100);
            }

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

            //first month is greater then second by x%
            return monthResult;
        }

        private (DateTime begin, DateTime end) GetWeekRange(string year)
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

            if (weekBegining > weekEnding)
            {
                monthEnding++;
            }

            var beginDate = new DateTime(int.Parse(year), monthBegining, weekBegining);
            var endDate = new DateTime(int.Parse(year), monthEnding, weekEnding, 23, 59, 59);

            return (beginDate, endDate);
        }

        #endregion
    }
}
