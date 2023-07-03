using Application.Exceptions;
using Domain.Categories;
using Domain.Entities.Base;
using Domain.Entities.Models;
using Domain.Modules;
using Domain.Modules.Queries;
using Domain.ValueObjects;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.Expenses.Queries
{
    public class ExpensesRepositoryQuery : DatabaseModule, IExpensesModuleQuery
    {
        private readonly IUserContextModule _userContext;

        public ExpensesRepositoryQuery(ExpenseDbContext context, IUserContextModule userContext) : base(context)
        {
            _userContext = userContext;
        }

        public async Task<UserIncome> GetMonthlyIncome(int id, string year, string month)
        {
            var model = await _context.UserIncomes.FirstOrDefaultAsync(m => m.UserExpensesListId == id
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            return model;
        }

        public async Task<UserExpense> GetExpense(int id)
        {
            var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);

            if (expense == null)
                throw new NotFoundException("Expense not found.");

            return expense;
        }

        public async Task<PagedList<UserExpenseResponseDto>> GetExpenses(int id, int? page, int? pagesize, CancellationToken token)
        {
            var pageNo = page ?? 1;
            var elemets = pagesize ?? 30;

            var userId = _userContext.GetUserId();

            if (userId == null)
                throw new NotFoundException("User not found.");

            //IQueryable<List<UserExpense>> model = _context.ExpensesLists
            //    .Where(e => e.Id == id && e.UserApplicationId == userId).Take(1)
            //    .Select(e => e.Expenses);

            IQueryable<UserExpense> collection = _context.Expenses
               .Where(e => e.UserExpensesListId == id && e.UserExpensesList.UserApplicationId == userId);

            IQueryable<UserExpenseResponseDto> model = collection
                .Select(x => new UserExpenseResponseDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.GetEnumDisplayName(),
                    Price = x.Price,
                    CreatedDate = x.CreatedDate,
                }); 

            //var model = _context.ExpensesLists
            //     .Include(e => e.Expenses
            //     .OrderByDescending(o => o.CreatedDate)
            //.Skip(pageNo * elemets)
            //.Take(elemets))
            //.FirstOrDefaultAsync(e => e.Id == id
            //&& e.UserApplicationId == userId, token);     

            if (model is null)
                throw new NotFoundException("Expenses not found.");

            var response = await PagedList<UserExpenseResponseDto>.Create(model, pageNo, elemets, token);

            return response;
        }
    }
}
