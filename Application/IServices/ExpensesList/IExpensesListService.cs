using Application.Dto.Models.Expenses;
using Application.Dto.Models.ExpensesList;
using Application.Dto.Models.Helpers;
using Domain.ValueObjects;

namespace Application.IServices.ExpensesList
{
    public interface IExpensesListService
    {
        IEnumerable<string> GetCategories();

        Task<IEnumerable<UserExpensesListDtoList>> GetExpensesLists();

        Task<UserExpensesListDtoModel> GetExpensesList(int id);

        Task<IEnumerable<UserExpensesDto>> GetExpensesByDate(DateTimeWithIdRequestModel request);

        Task<IEnumerable<DateComparer>> GetExpensesByDate(ExtendedDateTimeRequestModel request);

        Task CreateExpensesList(UserExpensesListModel model);

        Task UpdateExpensesList(UserExpensesListModel model, int id);

        Task DeleteExpensesList(int id);
    }
}
