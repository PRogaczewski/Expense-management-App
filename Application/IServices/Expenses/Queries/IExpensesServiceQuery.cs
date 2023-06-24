using Application.Dto.Models.Expenses;
using Domain.ValueObjects;

namespace Application.IServices.Expenses.Queries
{
    public interface IExpensesServiceQuery
    {
        ValueTask<UserIncomeDto> GetMonthlyIncome(int id, string year, string month);
    }
}
