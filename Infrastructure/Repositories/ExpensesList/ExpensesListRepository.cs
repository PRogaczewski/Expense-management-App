using Application.Exceptions;
using Domain.Categories;
using Domain.Entities.Models;
using Domain.Modules;
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

        public IEnumerable<UserExpensesList> GetExpensesLists()
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new BusinessException("Something went wrong...", 404);

            var models = _context.ExpensesLists
                //.Include(e => e.Expenses)
                //.OrderByDescending(o => o.CreatedDate.Year)
                //.ThenByDescending(o => o.CreatedDate.Month)
                //.Include(e=>e.UserGoals)
                //.ThenInclude(u => u.UserCategoryGoals)
                //.OrderBy(e => e.CreatedDate)
                //.Include(e=>e.UserIncomes)
                .Where(e => e.UserApplicationId == userId);

            if (models is null)
                throw new NotFoundException("Expenses lists not found.");

            return models;
        }

        public UserExpensesList GetExpensesList(int id)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new BusinessException("Something went wrong...", 404);

            var model = _context.ExpensesLists            
                .Include(e => e.Expenses
                .OrderByDescending(o => o.CreatedDate.Year)
                .ThenByDescending(o => o.CreatedDate.Month))
                .Include(e => e.UserGoals)
                .ThenInclude(u=>u.UserCategoryGoals)
                .Include(e=>e.UserIncomes)
                .FirstOrDefault(e => e.Id == id && e.UserApplicationId == userId);

            if (model is null)
                throw new NotFoundException("User expenses list not found.");

            return model;
        }

        public void CreateExpensesList(UserExpensesList model)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new BusinessException("Something went wrong...", 404);

            if (_context.ExpensesLists.Any(e => e.Name == model.Name && e.UserApplicationId == userId))
                throw new BusinessException("List with the same name already exists.", 409);

            if (model.Name.ToLower().Contains("seeder"))
            {
                model = _seeder.Seed(model.Name);
            }

            model.CreatedDate = DateTime.Now;
            model.UserApplicationId = userId.Value;

            _context.ExpensesLists.Add(model);
            _context.SaveChanges();
        }

        public void UpdateExpensesList(UserExpensesList model, int id)
        {
            var editModel = _context.ExpensesLists
                .Include(e => e.Expenses)
                .FirstOrDefault(m => m.Id == id);

            if (editModel is null)
                throw new NotFoundException("User list not found.");

            if (GetExpensesLists().Any(e => e.Name == model.Name))
                throw new BusinessException("List with this name exists.", 409);

            var userId = _userContext.GetUserId();

            if (userId == null || editModel.UserApplicationId != userId)
                throw new BusinessException("Something went wrong...", 404);

            editModel.Name = model.Name;
            editModel.UpdateDate = DateTime.Now;

            _context.SaveChanges();
        }

        public void DeleteExpensesList(int id)
        {
            var result = _context.ExpensesLists.First(e => e.Id == id);

            if (result is null)
                throw new NotFoundException("Expense list not found.");

            var userId = _userContext.GetUserId();

            if (userId == null || result.UserApplicationId != userId)
                throw new BusinessException("Something went wrong...", 404);

            _context.Remove(result);
            _context.SaveChanges();
        }
    }
}
