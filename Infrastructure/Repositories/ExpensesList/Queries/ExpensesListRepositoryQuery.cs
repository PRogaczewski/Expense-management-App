using Application.Exceptions;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.Modules.Queries;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.ExpensesList.Queries
{
    public class ExpensesListRepositoryQuery : DatabaseModule, ICategoryService, IExpensesListModuleQuery
    {
        private readonly IUserContextModule _userContext;

        public ExpensesListRepositoryQuery(ExpenseDbContext context, IUserContextModule userContext) : base(context)
        {
            _userContext = userContext;
        }

        public IEnumerable<string> GetCategories()
        {
            return ((ICategoryService)this).GetExpenseCategories();
        }

        public async Task<IEnumerable<UserExpensesList>> GetExpensesLists()
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var models = await _context.ExpensesLists
                .Where(e => e.UserApplicationId == userId).ToListAsync();

            if (models is null || !models.Any())
                throw new NotFoundException("Expenses lists not found.");

            return models;
        }

        public async Task<UserExpensesList> GetExpensesList(int id)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var model = await _context.ExpensesLists
                .Include(e => e.Expenses
                .OrderByDescending(o => o.CreatedDate.Year)
                .ThenByDescending(o => o.CreatedDate.Month))
                .Include(e => e.UserGoals)
                .ThenInclude(u => u.UserCategoryGoals)
                .Include(e => e.UserIncomes)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserApplicationId == userId);

            if (model is null)
                throw new NotFoundException("User expenses list not found.");

            return model;
        }

        public async Task<UserExpensesList> GetExpensesList(DateTimeWithIdRequestModel request)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var model = await _context.ExpensesLists
                .Include(e => e.Expenses
                .Where(x => (string.IsNullOrEmpty(request.Year) || x.CreatedDate.Year.ToString() == request.Year)
                 && (string.IsNullOrEmpty(request.Month) || x.CreatedDate.Month.ToString() == request.Month))
                .OrderByDescending(o => o.CreatedDate.Year)
                .ThenByDescending(o => o.CreatedDate.Month))
                .Include(e => e.UserGoals
                .Where(x => (string.IsNullOrEmpty(request.Year) || x.CreatedDate.Year.ToString() == request.Year)
                 && (string.IsNullOrEmpty(request.Month) || x.CreatedDate.Month.ToString() == request.Month)))
                .ThenInclude(u => u.UserCategoryGoals)
                .Include(e => e.UserIncomes)
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserApplicationId == userId);

            if (model is null)
                throw new NotFoundException("User expenses list not found.");

            return model;
        }

        public async Task<UserExpensesList> GetExpensesByDate(DateTimeWithIdRequestModel request)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var model = await _context.ExpensesLists
                 .Include(e => e.Expenses
                 .Where(x => (string.IsNullOrEmpty(request.Year) || x.CreatedDate.Year.ToString() == request.Year)
                 && (string.IsNullOrEmpty(request.Month) || x.CreatedDate.Month.ToString() == request.Month))
                 .OrderByDescending(o => o.CreatedDate))
                 .FirstOrDefaultAsync(e => e.Id == request.Id
                 && e.UserApplicationId == userId);

            if (model is null)
                throw new NotFoundException("Expenses list not found.");

            return model;
        }

        public async Task<UserExpensesList> GetExpensesByDate(ExtendedDateTimeRequestModel request)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            var model = await _context.ExpensesLists
                .Include(e => e.Expenses
                  .Where(e => (string.IsNullOrEmpty(request.Year) || e.CreatedDate.Year.ToString() == request.Year)
                 && (string.IsNullOrEmpty(request.Month) || e.CreatedDate.Month.ToString() == request.Month) ||
                 (string.IsNullOrEmpty(request.SecondYear) || e.CreatedDate.Year.ToString() == request.SecondYear)
                 && (string.IsNullOrEmpty(request.SecondMonth) || e.CreatedDate.Month.ToString() == request.SecondMonth)))
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.UserApplicationId == userId);

            if (model is null)
                throw new NotFoundException("Expenses not found.");

            return model;
        }
    }
}
