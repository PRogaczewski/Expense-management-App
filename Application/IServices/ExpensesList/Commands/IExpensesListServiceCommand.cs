using Application.Dto.Models.ExpensesList;

namespace Application.IServices.ExpensesList.Commands
{
    public interface IExpensesListServiceCommand
    {
        Task CreateExpensesList(UserExpensesListModel model);

        Task UpdateExpensesList(UserExpensesListModel model, int id);

        Task DeleteExpensesList(int id);
    }
}
