using Application.Exceptions;
using AutoMapper;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules.Commands;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.Expenses.Commands
{
    public class ExpensesRepositoryCommand : DatabaseModule, IExpensesModuleCommand
    {
        private readonly IMapper _mapper;

        public ExpensesRepositoryCommand(ExpenseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateExpense(UserExpense model)
        {
            model.CreatedDate = DateTime.Now;

            await _context.Expenses.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task CreateExpensesGoal(UserExpenseGoal model)
        {
            var result = _mapper.Map<UserExpenseGoal>(model);

            var existingCategories = new List<string>();

            var userGoals = _context.UserExpensesGoals.Where(u => u.MonthChosenForGoal == result.MonthChosenForGoal && u.UserExpensesListId == result.UserExpensesListId);

            foreach (var userGoal in result.UserCategoryGoals)
            {
                //IsAnySameCategory = userGoals.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category));
                if (userGoals.Any(u => u.UserCategoryGoals.Any(c => c.Category == userGoal.Category)))
                {
                    existingCategories.Add(userGoal.Category.GetEnumDisplayName().ToString());
                }
            }

            if (existingCategories.Any())
            {
                throw new GoalExistsException(existingCategories);
            }
            else
            {
                result.CreatedDate = DateTime.Now;

                await _context.UserExpensesGoals.AddAsync(result);
                await _context.SaveChangesAsync();
            }
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

            if (userGoal != null)
            {
                _context.UserExpensesGoals.Remove(userGoal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateExpensesGoal(UserExpenseGoal model)
        {
            throw new NotImplementedException();
        }
    }
}
