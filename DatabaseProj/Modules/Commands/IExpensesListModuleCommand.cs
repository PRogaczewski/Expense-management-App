using Domain.Entities.Models;

namespace Domain.Modules.Commands
{
    public interface IExpensesListModuleCommand
    {
        /// <summary>
        /// Create new expenses list
        /// </summary>
        /// <param name="model"></param>
        Task CreateExpensesList(UserExpensesList model);

        /// <summary>
        /// Update exisitng expenses list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        Task UpdateExpensesList(UserExpensesList model, int id);

        /// <summary>
        /// Delete current expenses list
        /// </summary>
        /// <param name="id"></param>
        Task DeleteExpensesList(int id);
    }
}
