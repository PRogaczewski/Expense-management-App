using ExpensesApi.Models.Categories;
using ServiceProj.Models.Model.Expenses;
using ServiceProj.ValidationService.Expenses;
using ServiceProj.ValidationService.ExpensesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.AplicationService.Expenses
{
    public class UserExpensesService : IUserExpensesService, IUserExpensesAnalysisService
    {
        private readonly IExpensesValidation _expensesValidation;

        private readonly IExpensesListValidation _expensesListValidation;

        public UserExpensesService(IExpensesValidation expensesValidation, IExpensesListValidation expensesListValidation)
        {
            _expensesValidation = expensesValidation;
            _expensesListValidation = expensesListValidation;
        }

        #region ExpensesAnalysis
        public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryCurrentWeek(int id, string year, string month)
        {
            var currentDay = DateTime.Now.Day;
            int weekBegining;

            if (DateTime.Now.DayOfWeek == 0)
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek - 6).Day;
            else
                weekBegining = DateTime.Today.AddDays(-1 * (int)DateTime.Today.DayOfWeek + 1).Day;


            var models = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month && e.CreatedDate.Day >= weekBegining && e.CreatedDate.Day <= currentDay)
                .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryMonth(int id, string year, string month)
        {
            var models = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<ExpenseCategories, decimal> ExpensesByCategoryYear(int id, string year)
        {
            var models = _expensesListValidation.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();

            return ExpensesByCategory(id, models);
        }

        public IDictionary<ExpenseCategories, decimal> CompareByCategoryMonth(int id, string firstYear, string secondYear, string firstMonth, string secondMonth)
        {
            var firstMonthModels = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear && e.CreatedDate.Month.ToString() == firstMonth)
                .ToList();

            var secondMonthModels = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear && e.CreatedDate.Month.ToString() == secondMonth)
                .ToList();

            var firstMonthResult = ExpensesByCategory(id, firstMonthModels);
            var secondMonthResult = ExpensesByCategory(id, secondMonthModels);

            return CompareByCategory(firstMonthResult, secondMonthResult);
        }

        public IDictionary<ExpenseCategories, decimal> CompareByCategoryYear(int id, string firstYear, string secondYear)
        {
            var firstYearModels = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == firstYear)
                .ToList();

            var secondYearModels = _expensesListValidation.GetExpensesList(id).Expenses
                .Where(e => e.CreatedDate.Year.ToString() == secondYear)
                .ToList();

            var firstYearResult = ExpensesByCategory(id, firstYearModels);
            var secondYearResult = ExpensesByCategory(id, secondYearModels);

            return CompareByCategory(firstYearResult, secondYearResult);
        }

        public IDictionary<ExpenseCategories, decimal>[] MonthlyGoals(int id, string year, string month)
        {
            var currentDate = month + year;

            var currentGoals = _expensesListValidation.
                GetExpensesList(id).UserGoals.
                Where(g => g.MonthChosenForGoal.Month.ToString() + g.MonthChosenForGoal.Year.ToString() == currentDate)
                .Where(g => g.UserExpensesListId == id)
                .ToList();

            IDictionary<ExpenseCategories, decimal> currentMonthGoal = new Dictionary<ExpenseCategories, decimal>();

            foreach (var goalsList in currentGoals)
            {
                foreach (var item in goalsList.UserCategoryGoals)
                {
                    if (currentMonthGoal.ContainsKey(item.Category))
                    {
                        currentMonthGoal[item.Category] += item.Limit;
                    }
                    else
                    {
                        currentMonthGoal.Add(item.Category, item.Limit);
                    }
                }
            }

            var currentMonthExpenses = ExpensesByCategoryMonth(id, year, month);

            //IDictionary<ExpenseCategories, decimal> currentMonthGoal = new Dictionary<ExpenseCategories, decimal>();

            //foreach (var goal in currentGoals)
            //{
            //    currentMonthGoal.Add(goal.Category, goal.Limit);
            //}

            var categories = currentMonthGoal.Keys.Intersect(currentMonthExpenses.Keys);

            IDictionary<ExpenseCategories, decimal> currentMonthResult = new Dictionary<ExpenseCategories, decimal>();
            IDictionary<ExpenseCategories, decimal> currentMonthGoalExpenses = new Dictionary<ExpenseCategories, decimal>();
            IDictionary<ExpenseCategories, decimal> currentMonthResultPercentage = new Dictionary<ExpenseCategories, decimal>();

            foreach (var category in categories)
            {
                if (currentMonthGoal.TryGetValue(category, out var firstDecimal) && currentMonthExpenses.TryGetValue(category, out var secondDecimal))
                {
                    currentMonthGoalExpenses.Add(category, secondDecimal);
                    currentMonthResult.Add(category, decimal.Round(firstDecimal - secondDecimal, 2));
                    currentMonthResultPercentage.Add(category, decimal.Round(1 - (secondDecimal / firstDecimal) * 100, 2));
                }
            }

            //return new IDictionary<ExpenseCategories, decimal>[] { currentMonthResult, currentMonthResultPercentage };
            return new IDictionary<ExpenseCategories, decimal>[] { currentMonthGoal, currentMonthGoalExpenses, currentMonthResult };
        }

        public decimal TotalIncomesMonth(int id, string year, string month)
        {
            var income = GetMonthlyIncome(id, year, month);

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
                models = _expensesListValidation.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year)
                    .ToList();
            }
            else
            {
                models = _expensesListValidation.GetExpensesList(id).Expenses
                    .Where(e => e.CreatedDate.Year.ToString() == year && e.CreatedDate.Month.ToString() == month)
                    .ToList();
            }

            if (models is null)
                throw new Exception();

            decimal totalSum = 0.0m;

            foreach (var expense in models)
            {
                totalSum += expense.Price;
            }

            return decimal.Round(totalSum, 2);
        }

        private IDictionary<ExpenseCategories, decimal> ExpensesByCategory(int id, IList<UserExpensesDto> models)
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
                .ToDictionary(t => t.Key, t => t.Value);
        }

        private IDictionary<ExpenseCategories, decimal> CompareByCategory(IDictionary<ExpenseCategories, decimal> firstMonthResult, IDictionary<ExpenseCategories, decimal> secondMonthResult)
        {
            //Get the same categories
            var categories = firstMonthResult.Keys.Intersect(secondMonthResult.Keys);

            IDictionary<ExpenseCategories, decimal> monthResult = new Dictionary<ExpenseCategories, decimal>();

            foreach (var category in categories)
            {
                if (firstMonthResult.TryGetValue(category, out var firstDecimal) && secondMonthResult.TryGetValue(category, out var secondDecimal))
                {
                    monthResult.Add(category, decimal.Round((1 - (firstDecimal / secondDecimal)) * 100 * -1, 1));
                }
            }

            return monthResult;
        }
        #endregion

        #region ExpensesService
        public UserExpensesDto GetExpensesList(int id)
        {
            try
            {
                return _expensesValidation.GetExpensesList(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateExpensesList(UserExpensesModel model)
        {
            try
            {
                _expensesValidation.CreateExpensesList(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateExpensesGoal(UserExpenseGoalDto model)
        {
            try
            {
                _expensesValidation.CreateExpensesGoal(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month)
        {
            try
            {
                return _expensesValidation.GetMonthlyIncome(id, year, month);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddMonthlyIncome(UserIncomeModel income)
        {
            try
            {
                _expensesValidation.AddMonthlyIncome(income);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
