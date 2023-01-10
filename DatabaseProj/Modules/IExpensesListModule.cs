using Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules
{
    public interface IExpensesListModule
    {
        /// <summary>
        /// Get all available categories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetCategories();

        /// <summary>
        /// Get all user expenses lists
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserExpensesList> GetExpensesLists();

        /// <summary>
        /// Get current expenses list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserExpensesList GetExpensesList(int id);

        /// <summary>
        /// Create new expenses list
        /// </summary>
        /// <param name="model"></param>
        public void CreateExpensesList(UserExpensesList model);

        /// <summary>
        /// Update exisitng expenses list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        public void UpdateExpensesList(UserExpensesList model, int id);

        /// <summary>
        /// Delete current expenses list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteExpensesList(int id);
    }
}
