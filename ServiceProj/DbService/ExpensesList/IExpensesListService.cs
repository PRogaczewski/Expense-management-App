using ServiceProj.Models.Model.ExpensesList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.DbService.ExpensesList
{
    public interface IExpensesListService
    {
        public IEnumerable<UserExpensesListDtoList> GetExpensesLists();

        public UserExpensesListDtoModel GetExpensesList(int id);

        public void CreateExpensesList(UserExpensesListModel model);

        public void UpdateExpensesList(UserExpensesListModel model, int id);

        public void DeleteExpensesList(int id);
    }
}
