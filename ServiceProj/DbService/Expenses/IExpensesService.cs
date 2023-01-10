using ServiceProj.Models.Model.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProj.DbService.Expenses
{
    public interface IExpensesService
    {
        public UserExpensesDto GetExpensesList(int id);

        public void CreateExpensesList(UserExpensesModel model);

        public bool CreateExpensesGoal(UserExpenseGoalDto model);

        public UserIncomeDto GetMonthlyIncome(int id, string year, string month);

        public void AddMonthlyIncome(UserIncomeModel income); 
    }
}
