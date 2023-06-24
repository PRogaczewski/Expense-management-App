using Domain.Entities.Models;
using Domain.ValueObjects;

namespace Domain.Modules.Queries
{
    public interface IExpensesListModuleQuery
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
        /// Get current expenses list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserExpensesList> GetExpensesList(DateTimeWithIdRequestModel request);

        /// <summary>
        /// Get expenses from current date time option
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserExpensesList> GetExpensesByDate(DateTimeWithIdRequestModel request);

        /// <summary>
        /// Get expenses from current extended date time option
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserExpensesList> GetExpensesByDate(ExtendedDateTimeRequestModel request);

        Task<IEnumerable<UserExpense>> GetExpenses(int id, int? page, int? pagesize, CancellationToken token);
    }
}
