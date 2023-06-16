using Application.Exceptions;
using AutoMapper;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.Expenses
{
    public class ExpensesRepository : IExpensesModule
    {
        private readonly ExpenseDbContext _context;

        private readonly IMapper _mapper;

        public ExpensesRepository(ExpenseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Query
        public async Task<UserIncome> GetMonthlyIncome(int id, string year, string month)
        {
            var model = await _context.UserIncomes.FirstOrDefaultAsync(m => m.UserExpensesListId == id
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            return model;
        }
        #endregion

        #region Command

        public async Task CreateExpense(UserExpense model)
        {
            model.CreatedDate = DateTime.Now;

            await _context.Expenses.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateExpensesGoal(UserExpenseGoal model)
        {
            var result = _mapper.Map<UserExpenseGoal>(model);

            //bool IsAnySameCategory = false;
            var existingCategories = new List<string>();

            var userGoals = _context.UserExpensesGoals.Where(u => u.MonthChosenForGoal == result.MonthChosenForGoal && u.UserExpensesListId == result.UserExpensesListId);

            foreach (var userGoal in result.UserCategoryGoals)
            {
                //IsAnySameCategory = userGoals.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category));
                if(userGoals.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category)))
                {
                    existingCategories.Add(userGoal.Category.GetEnumDisplayName().ToString());
                }
            }

            if(existingCategories.Any())
            {
                throw new GoalExistsException(existingCategories);
            }

            if (!existingCategories.Any())
            {
                result.CreatedDate = DateTime.Now;

                await _context.UserExpensesGoals.AddAsync(result);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task AddMonthlyIncome(UserIncome income)
        {
            var currentMonthIncomes = await _context.UserIncomes.FirstOrDefaultAsync(u => u.UserExpensesListId == income.UserExpensesListId && u.CreatedDate.Month == DateTime.Now.Month);

            if (currentMonthIncomes != null)
            {
                currentMonthIncomes.Income += income.Income;
                currentMonthIncomes.UpdateDate = DateTime.Now;
            }
            else
            {
                income.CreatedDate = DateTime.Now;

                await _context.UserIncomes.AddAsync(income);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpensesGoal(DateTimeWithIdRequestModel model)
        {
            var userGoal = await _context.UserExpensesGoals
                .FirstOrDefaultAsync(u => u.MonthChosenForGoal.Month.ToString() == model.Month 
                && u.MonthChosenForGoal.Year.ToString() == model.Year 
                && u.UserExpensesListId == model.Id);

            if(userGoal != null)
            {
                _context.UserExpensesGoals.Remove(userGoal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateExpensesGoal(UserExpenseGoal model)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
