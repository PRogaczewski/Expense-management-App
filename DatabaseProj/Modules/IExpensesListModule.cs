using Domain.Entities.Models;

namespace Domain.Modules
{
    public interface IExpensesListModule
    {
        /// <summary>
        /// Get all available categories
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetCategories();

        /// <summary>
        /// Get all user expenses lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<UserExpensesList>> GetExpensesLists();

        /// <summary>
        /// Get current expenses list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserExpensesList> GetExpensesList(int id);

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
