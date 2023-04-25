using Application.Dto.Models.ExpensesList;

namespace Application.IServices.ExpensesList
{ 
    public interface IExpensesListService
    {
        IEnumerable<string> GetCategories();

        Task<IEnumerable<UserExpensesListDtoList>> GetExpensesLists();

        Task<UserExpensesListDtoModel> GetExpensesList(int id);

        Task CreateExpensesList(UserExpensesListModel model);

        Task UpdateExpensesList(UserExpensesListModel model, int id);

        Task DeleteExpensesList(int id);
    }
}
