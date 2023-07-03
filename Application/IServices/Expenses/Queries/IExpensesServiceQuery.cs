using Application.Dto.Models.Expenses;
using Domain.Entities.Base;
using Domain.ValueObjects;

namespace Application.IServices.Expenses.Queries
{
    public interface IExpensesServiceQuery
    {
        Task<UserIncomeDto> GetMonthlyIncome(int id, string year, string month);

        Task<UserExpensesDto> GetExpense(int id);

        Task<PagedList<UserExpenseResponseDto>> GetExpenses(int id, int? page, int? pagesize, CancellationToken token);
    }
}
