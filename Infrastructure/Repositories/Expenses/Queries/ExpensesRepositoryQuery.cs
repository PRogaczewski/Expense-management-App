using Domain.Entities.Models;
using Domain.Modules.Queries;
using Infrastructure.EF.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repositories.Expenses.Queries
{
    public class ExpensesRepositoryQuery : DatabaseModule, IExpensesModuleQuery
    {
        public ExpensesRepositoryQuery(ExpenseDbContext context)
        {
            _context = context;
        }

        public async Task<UserIncome> GetMonthlyIncome(int id, string year, string month)
        {
            var model = await _context.UserIncomes.FirstOrDefaultAsync(m => m.UserExpensesListId == id
            && m.CreatedDate.Year.ToString() == year
            && m.CreatedDate.Month.ToString() == month);

            return model;
        }
    }
}
