using Application.Exceptions;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Infrastructure.SeedData.Service;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.ExpensesList
{
    public class ExpensesListRepository : ICategoryService, IExpensesListModule
    {
        private readonly ExpenseDbContext _context;

        private readonly IExpensesSeeder _seeder;

        private readonly IUserContextModule _userContext;

        public ExpensesListRepository(ExpenseDbContext context, IExpensesSeeder seeder, IUserContextModule userContext)
        {
            _context = context;
            _seeder = seeder;
            _userContext = userContext;
        }

        public IEnumerable<string> GetCategories()
        {
            return ((ICategoryService)this).GetExpenseCategories();
        }

        #region Query

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
                 .OrderByDescending(o => o.CreatedDate.Year)
                 .ThenByDescending(o => o.CreatedDate.Month))
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
            //.Where(e => e.CreatedDate.Year.ToString() == request.Year
            //&& e.CreatedDate.Month.ToString() == request.Month ||
            //e.CreatedDate.Year.ToString() == request.SecondYear && e.CreatedDate.Month.ToString() == request.SecondMonth))

            if (model is null)
                throw new NotFoundException("Expenses not found.");

            return model;
        }

        #endregion

        #region Command

        public async Task CreateExpensesList(UserExpensesList model)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            if (_context.ExpensesLists.Any(e => e.Name == model.Name && e.UserApplicationId == userId))
                throw new BusinessException("List with the same name already exists.", 409);

            if (model.Name.ToLower().Contains("seeder"))
            {
                model = _seeder.Seed(model.Name);
            }

            model.CreatedDate = DateTime.Now;
            model.UserApplicationId = userId.Value;

            await _context.ExpensesLists.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExpensesList(UserExpensesList model, int id)
        {
            var editModel = _context.ExpensesLists
                .Include(e => e.Expenses)
                .FirstOrDefault(m => m.Id == id);

            if (editModel is null)
                throw new NotFoundException("User list not found.");

            if ((await GetExpensesLists()).Any(e => e.Name == model.Name))
                throw new BusinessException("List with this name exists.", 409);

            var userId = _userContext.GetUserId();

            if (userId == null || editModel.UserApplicationId != userId)
                throw new BusinessException("Something went wrong...", 404);

            editModel.Name = model.Name;
            editModel.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpensesList(int id)
        {
            var result = _context.ExpensesLists.First(e => e.Id == id);

            if (result is null)
                throw new NotFoundException("Expense list not found.");

            var userId = _userContext.GetUserId();

            if (userId == null || result.UserApplicationId != userId)
                throw new NotFoundException("User not found.");

            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
