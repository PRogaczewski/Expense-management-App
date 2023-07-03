using Application.Exceptions;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.Modules.Commands;
using Infrastructure.EF.Database;
using Infrastructure.SeedData.Service;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.ExpensesList.Commands
{
    public class ExpensesListRepositoryCommand : DatabaseModule, IExpensesListModuleCommand
    {
        private readonly IExpensesSeeder _seeder;

        private readonly IUserContextModule _userContext;

        public ExpensesListRepositoryCommand(ExpenseDbContext context, IExpensesSeeder seeder, IUserContextModule userContext)
            : base(context)
        {
            _seeder = seeder;
            _userContext = userContext;
        }

        public async Task CreateExpensesList(UserExpensesList model)
        {
            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            if (await _context.ExpensesLists.AnyAsync(e => e.Name == model.Name && e.UserApplicationId == userId))
                throw new BusinessException("List with the same name already exists.", 409);

            if (_context.ExpensesLists.Where(e => e.UserApplicationId == userId).Count() == 5)
                throw new BusinessException("You may have up to five lists.", 400);

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
            var editModel = await _context.ExpensesLists
                .Include(e => e.Expenses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (editModel is null)
                throw new NotFoundException("User list not found.");

            if (await _context.ExpensesLists.AnyAsync(e => e.Name == model.Name))
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
    }
}
