using Domain.Entities.Base;
using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Domain.Modules.Queries
{
    public interface IExpensesModuleQuery
    {
        Task<UserIncome> GetMonthlyIncome(int id, string year, string month);

        Task<UserExpense> GetExpense(int id);

        Task<PagedList<UserExpenseResponseDto>> GetExpenses(int id, int? page, int? pagesize, CancellationToken token);
    }
}
