using AutoMapper;
using Domain.Entities.Models;
using Domain.Modules;
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

        public async Task CreateExpense(UserExpense model)
        {
            var result = _mapper.Map<UserExpense>(model);

            result.CreatedDate = DateTime.Now;

            await _context.Expenses.AddAsync(result);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CreateExpensesGoal(UserExpenseGoal model)
        {
            var result = _mapper.Map<UserExpenseGoal>(model);

            bool IsAnySameCategory = false;

            foreach (var userGoal in result.UserCategoryGoals)
            {
                var test = _context.UserExpensesGoals.Where(u => u.MonthChosenForGoal == result.MonthChosenForGoal && u.UserExpensesListId == result.UserExpensesListId).ToList();

                IsAnySameCategory = test.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category));
            }

            if (!IsAnySameCategory)
            {
                result.CreatedDate = DateTime.Now;

                await _context.UserExpensesGoals.AddAsync(result);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<UserIncome> GetMonthlyIncome(int id, string year, string month)
        {
            var model = await _context.UserIncomes.FirstOrDefaultAsync(m => m.UserExpensesListId == id 
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            return model;
        }

        public async Task AddMonthlyIncome(UserIncome income)
        {
            var currentMonthIncomes = await _context.UserIncomes.FirstOrDefaultAsync(u => u.UserExpensesListId == income.UserExpensesListId && u.CreatedDate.Month==DateTime.Now.Month);

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
    }
}
